using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.Application.Resources;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Repositories;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Services
{
    internal class DocumentService : IDocumentService
    {
        private readonly IBaseRepository<Document> _documentRepository;
        private readonly IDocumentValidator _documentValidator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DocumentService(IBaseRepository<Document> documentRepository,IDocumentValidator departmentValidator,IMapper mapper, ILogger logger)
        {
            _documentRepository = documentRepository;
            _documentValidator = departmentValidator;
            _mapper = mapper;
            _logger = logger;
        }

        ///<inheritdoc/>
        public async Task<CollectionResult<DocumentDto>> GetDocumentsAsync(DateTime dateStart, DateTime dateEnd)
        {
            DocumentDto[] documents;

            try
            {
                documents = await _documentRepository.GetAll().Where(x=>x.date>=dateStart && x.date<=dateEnd)
                    .Select(x=>new DocumentDto(x.Id,x.Value,x.Comment, x.ExpenditureID,
                    x.ExpenditureID,x.ExpenditureID)).ToArrayAsync(); // ToArrayAsync() - из библиотеки Microsoft.EntityFrameworkCore;
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
        public async Task<CollectionResult<DocumentDto>> GetDocumentsByExpenditureAsync(DateTime dateStart, DateTime dateEnd, short expenditureId)
        {
            DocumentDto[] documents;

            try
            {
                documents = await _documentRepository.GetAll().Where(x => x.date >= dateStart && x.date <= dateEnd && x.ExpenditureID == expenditureId)
                    .Select(x => new DocumentDto(x.Id,  x.Value, x.Comment, x.ExpenditureID, x.ExpenditureID, x.ExpenditureID)).ToArrayAsync(); // ToArrayAsync() - из библиотеки Microsoft.EntityFrameworkCore;
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
            DocumentDto document;

            try
            {
                document = await _documentRepository.GetAll()
                    .Select(x => new DocumentDto(x.Id, x.Value, x.Comment, x.ExpenditureID, x.ExpenditureID, x.ExpenditureID))
                    .FirstOrDefaultAsync(x=>x.Id==id);
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
                _logger.Warning("Документ с {Id} не найден",id);
                return new BaseResult<DocumentDto>()
                {
                    ErrorMessage = ErrorMessage.DocumentNotFound,
                    ErrorCode = (int)ErrorCodes.DocumentNotFound
                };
            }

            return new BaseResult<DocumentDto>() 
            {
                Data=document
            };
        }

        ///<inheritdoc/>
        public async Task<BaseResult<DocumentDto>> CreateDocumentAsync(CreateDocumentDto dto)
        {
            try
            {

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
            throw new NotImplementedException();
        }
    }
}
