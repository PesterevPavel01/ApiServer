using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.ObjectModel;
using WebApi.Application.Resources;
using WebApi.Application.Validations;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Order;
using WebApi.Domain.Dto.Report;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Repositories;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Application.Services
{
    internal class DocumentService : IDocumentService
    {
        private readonly IBaseRepository<Document> _documentRepository;
        private readonly IBaseRepository<Department> _departmentRepository;
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IBaseRepository<Expenditure> _expenditureRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Target> _targetRepository;
        private readonly IDocumentValidator _documentValidator;
        private readonly IExpenditureValidator _expenditureValidator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DocumentService(IBaseRepository<Document> documentRepository,
            IBaseRepository<Department>departmentRepository,
            IBaseRepository<Organization> organizationRepository,
            IBaseRepository<Expenditure> expenditureRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<Target> targetRepository,
            IDocumentValidator departmentValidator,
            IExpenditureValidator expenditureValidator,
            IMapper mapper, ILogger logger
            )
        {
            _documentRepository = documentRepository;
            _documentValidator = departmentValidator;
            _departmentRepository = departmentRepository;
            _organizationRepository = organizationRepository;
            _expenditureRepository = expenditureRepository;
            _targetRepository = targetRepository;
            _mapper = mapper;
            _logger = logger;
            _expenditureValidator = expenditureValidator;
            _userRepository = userRepository;
        }

        ///<inheritdoc/>
        public async Task<CollectionResult<DocumentDto>> GetDocumentsAsync(DateTime dateStart, DateTime dateEnd)
        {
            if(dateStart>dateEnd) return new CollectionResult<DocumentDto>() { ErrorMessage= ErrorMessage.IncorrectPeriod,ErrorCode=(int)ErrorCodes.IncorrectPeriod };
            
            DocumentDto[] documents;

            try
            {
                documents = await _documentRepository.GetAll().Where(x=>x.Date >=dateStart && x.Date <= dateEnd)
                    .Select(x=>new DocumentDto(x.Id,x.Value,x.Date,x.Comment, x.Expenditure.Name,
                    x.Organization.Name,x.Department.Name)).ToArrayAsync(); // ToArrayAsync() - из библиотеки Microsoft.EntityFrameworkCore;
            }
            catch(Exception ex) 
            {
                _logger.Error(ex,ex.Message);
                return new CollectionResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (!documents.Any())
            {
                _logger.Warning(ErrorMessage.DocumentsNotFound,documents.Length);
                return new CollectionResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.DocumentsNotFound,
                    ErrorCode = (int)ErrorCodes.DocumentsNotFound
                };
            }

            return new CollectionResult<DocumentDto>() 
            {
                Data=documents,
                Count=documents.Length
            };
        }

        ///<inheritdoc/>
        public async Task<CollectionResult<DocumentDto>> GetDocumentsByExpenditureAsync(DateTime dateStart, DateTime dateEnd, string expenditureName)
        {
            if (dateStart > dateEnd) return new CollectionResult<DocumentDto>() { ErrorMessage = ErrorMessage.IncorrectPeriod, ErrorCode = (int)ErrorCodes.IncorrectPeriod };

            DocumentDto[] documents;

            var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == expenditureName);
            var result = _expenditureValidator.CreateValidator(expenditure);

            if (!result.IsSuccess)
                return new CollectionResult<DocumentDto>
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                };

            try
            {
                documents = await _documentRepository.GetAll().Where(x => x.Date >= dateStart && x.Date <= dateEnd && x.ExpenditureID == expenditure.Id)
                    .Select(x => new DocumentDto(x.Id,  x.Value,x.Date, x.Comment, x.Expenditure.Name, x.Organization.Name, x.Department.Name)).ToArrayAsync(); // ToArrayAsync() - из библиотеки Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (!documents.Any())
            {
                _logger.Warning(ErrorMessage.DocumentsNotFound, documents.Length);
                return new CollectionResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.DocumentsNotFound,
                    ErrorCode = (int)ErrorCodes.DocumentsNotFound
                };
            }

            return new CollectionResult<DocumentDto>()
            {
                Data = documents,
                Count = documents.Length
            };
        }

        ///<inheritdoc/>
        public async Task<BaseResult<DocumentDto>> GetDocumentByIdAsync(long id)
        {
            Document document;

            try
            {
                document = await _documentRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (document==null)
            {
                _logger.Warning($"Документ с {id} не найден",id);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.DocumentNotFound,
                    ErrorCode = (int)ErrorCodes.DocumentNotFound
                };
            }

            var dto = new DocumentDto(document.Id, document.Value, document.Date, document.Comment);
            
            var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == document.DepartmentID);
            if (department != null) dto.Department = department.Name;

            var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == document.OrganizationID);
            if (organization != null) dto.Organization = organization.Name;

            var expenditure = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == document.ExpenditureID);
            if (expenditure != null) dto.Expenditure = expenditure.Name;

            return new BaseResult<DocumentDto>() 
            {
                Data=dto
            };
        }

        ///<inheritdoc/>
        public async Task<BaseResult<DocumentDto>> CreateDocumentAsync(CreateDocumentDto model)
        {
            if (model == null) return new BaseResult<DocumentDto>() { ErrorMessage = ErrorMessage.IncorrectInputObject, ErrorCode = (int)ErrorCodes.IncorrectInputObject };
            try
            {
                var document = new Document {
                    Value = model.Value,
                    Comment = model.Comment,
                    Date = model.Date
                };

                var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Department);
                if(department != null) document.DepartmentID = department.Id;

                var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Organization);
                if(organization != null)  document.OrganizationID = organization.Id;

                var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Expenditure);
                if(expenditure != null) document.ExpenditureID =  expenditure.Id ;

                await _documentRepository.CreateAsync(document);

                return new BaseResult<DocumentDto>()
                {
                    Data = _mapper.Map<DocumentDto>(document),
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        ///<inheritdoc/>
        public async Task<BaseResult<DocumentDto>> CreateDocumentsMultipleAsync(List<CreateDocumentDto> listModels)
        {
            if (listModels == null) return new BaseResult<DocumentDto>() { ErrorMessage = ErrorMessage.IncorrectInputObject, ErrorCode = (int)ErrorCodes.IncorrectInputObject };

            List<Document> newDocuments = new();
            try
            {
                foreach (var documentModel in listModels)
                {
                    var document = new Document
                    {
                        Value = documentModel.Value,
                        Comment = documentModel.Comment,
                        Date = documentModel.Date
                    };

                    var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Name == documentModel.Department);
                    if (department != null) document.DepartmentID = department.Id;

                    var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Name == documentModel.Organization);
                    if (organization != null) document.OrganizationID = organization.Id;

                    var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == documentModel.Expenditure);
                    if (expenditure != null) document.ExpenditureID = expenditure.Id;

                    newDocuments.Add(document);
                }

                if (newDocuments.Count == 0)
                    return new BaseResult<DocumentDto>()
                    {
                        ErrorMessage = ErrorMessage.NewDocumentsNotFound,
                        ErrorCode = (int)ErrorCodes.NewDocumentsNotFound
                    };

                await _documentRepository.CreateMultipleAsync(newDocuments);

                return new BaseResult<DocumentDto>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<DocumentDto>> UpdateDocumentAsync(DocumentDto model)
        {
            if (model == null) return new BaseResult<DocumentDto>() { ErrorMessage = ErrorMessage.IncorrectInputObject, ErrorCode = (int)ErrorCodes.IncorrectInputObject };

            try
            {
                var document = await _documentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                var result = _documentValidator.ValidateOrNull(document);

                if (!result.IsSuccess)
                    return new BaseResult<DocumentDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Department);
                if (department != null)
                {
                    document.DepartmentID = department.Id; 
                    document.Department = department;
                }

                var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Organization);
                if (organization != null) 
                { 
                    document.OrganizationID = organization.Id;
                    document.Organization = organization;
                }

                var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Expenditure);
                if (expenditure != null)
                {
                    document.ExpenditureID = expenditure.Id;
                    document.Expenditure = expenditure;
                }

                document.Comment = model.Comment;
                document.Date = model.Date;
                document.Value = model.Value;

                await _documentRepository.UpdateAsync(document);

                return new BaseResult<DocumentDto>()
                {
                    Data = _mapper.Map<DocumentDto>(document),
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        ///<inheritdoc/>
        public async Task<BaseResult<DocumentDto>> DeleteDocumentAsync(long id)
        {
            try
            {
                var document = await _documentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _documentValidator.ValidateOrNull(document);

                if (!result.IsSuccess)
                    return new BaseResult<DocumentDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                await _documentRepository.RemoveAsync(document);

                return new BaseResult<DocumentDto>()
                {
                    Data = new DocumentDto(document),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<CollectionResult<ExpenseReportDto>> GetReportAsync(ReportArgument argument)
        {

            try
            {
                var documents= await _documentRepository.GetAll().Where(x => x.Date.Year == argument.Year && x.Date.Month == argument.Month).ToListAsync();

                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x=>x.Login== argument.Login && x.Password==argument.Password);

                if (user==null)
                    return new CollectionResult<ExpenseReportDto>
                    {
                        ErrorMessage = ErrorMessage.UserNotFound,
                        ErrorCode = (int)ErrorCodes.UserNotFound,
                    };

                var subReport = from d in documents
                             join e in _expenditureRepository.GetAll().Where(x => x.UserID == user.Id)
                             on d.ExpenditureID equals e.Id
                             select new { Value = d.Value, Expenditure = e.Id, ExpenditureName=e.Name};


                var report = from sr in subReport
                             join t in _targetRepository.GetAll().Where(x => x.Month == argument.Month && x.Year == argument.Year)
                             on sr.Expenditure equals t.ExpenditureID into tblExpend
                             from resTbl in tblExpend.DefaultIfEmpty()
                             select new { Value = sr.Value, Expenditure = sr.ExpenditureName, Target = resTbl?.Value??0 };

                if (!report.Any())
                {
                    return new CollectionResult<ExpenseReportDto>()
                    {
                        ErrorMessage = ErrorMessage.DocumentsNotFound,
                        ErrorCode = (int)ErrorCodes.DocumentsNotFound
                    };
                }

                List<ExpenseReportDto> expenseReport = report
                    .GroupBy(x=>x.Expenditure)
                    .Select(er=>
                    new ExpenseReportDto(er.First().Expenditure,er.Sum(d=>d.Value),user.Name,er.First().Target)
                    ).ToList();

                return new CollectionResult<ExpenseReportDto>
                {
                    Count = expenseReport.Count(),
                    Data = expenseReport,
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<ExpenseReportDto>
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                };
            }
        }
    }
}
