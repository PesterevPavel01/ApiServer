using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.Application.Resources;
using WebApi.Application.Validations;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Repositories;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Services
{
    internal class ExpenditureService: ICommonService<ExpenditureDto,short>
    {
        private readonly IBaseRepository<Expenditure> _expenditureRepository;
        private readonly IExpenditureValidator _expenditureValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ExpenditureService(IBaseRepository<Expenditure> expenditureRepository, IExpenditureValidator expenditureValidator, IMapper mapper, ILogger logger)
        {
            _expenditureRepository = expenditureRepository;
            _mapper = mapper;
            _logger = logger;
            _expenditureValidator = expenditureValidator;

        }

        public async Task<BaseResult<ExpenditureDto>> CreateAsync(ExpenditureDto model)
        {
            try
            {
                var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                var result = _expenditureValidator.CreateValidator(expenditure);

                if (!result.IsSuccess)
                    return new BaseResult<ExpenditureDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                expenditure = new Expenditure
                {
                    Name = model.Name
                };

                await _expenditureRepository.CreateAsync(expenditure);

                return new BaseResult<ExpenditureDto>()
                {
                    Data = _mapper.Map<ExpenditureDto>(expenditure),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ExpenditureDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<ExpenditureDto>> CreateMultiple(List<ExpenditureDto> listModel)
        {
            List<Expenditure> newExpenditures = new List<Expenditure>();
            try
            {
                foreach (var expenditureDto in listModel)
                {
                    var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == expenditureDto.Name);
                    var result = _expenditureValidator.CreateValidator(expenditure);

                    if (!result.IsSuccess) continue;

                    newExpenditures.Add(_mapper.Map<Expenditure>(expenditureDto));

                }

                if (newExpenditures.Count == 0)
                    return new BaseResult<ExpenditureDto>()
                    {
                        ErrorMessage = ErrorMessage.NewExpendituresNotFound,
                        ErrorCode = (int)ErrorCodes.NewExpendituresNotFound
                    };

                await _expenditureRepository.CreateMultipleAsync(newExpenditures);

                return new BaseResult<ExpenditureDto>();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ExpenditureDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
        public async Task<BaseResult<ExpenditureDto>> UpdateAsync(ExpenditureDto model)
        {
            try
            {
                var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                var result = _expenditureValidator.ValidateOrNull(expenditure);

                if (!result.IsSuccess)
                    return new BaseResult<ExpenditureDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                expenditure.Name = model.Name;

                await _expenditureRepository.UpdateAsync(expenditure);

                return new BaseResult<ExpenditureDto>()
                {
                    Data = _mapper.Map<ExpenditureDto>(expenditure),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ExpenditureDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<ExpenditureDto>> DeleteAsync(short id)
        {
            try
            {
                var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _expenditureValidator.ValidateOrNull(expenditure);

                if (!result.IsSuccess)
                    return new BaseResult<ExpenditureDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                await _expenditureRepository.RemoveAsync(expenditure);

                return new BaseResult<ExpenditureDto>()
                {
                    Data = _mapper.Map<ExpenditureDto>(expenditure),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ExpenditureDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<CollectionResult<ExpenditureDto>> GetAllAsync()
        {
            ExpenditureDto[] expenditures;
            try
            {
                expenditures = await _expenditureRepository.GetAll()
                    .Select(x => new ExpenditureDto(x.Id, x.Name)).ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<ExpenditureDto>()
                {
                    ErrorMessage = ErrorMessage.ExpenditureNotFound,
                    ErrorCode = (int)ErrorCodes.ExpenditureNotFound
                };
            }

            return new CollectionResult<ExpenditureDto>()
            {
                Data = expenditures,
                Count = expenditures.Length
            };
        }

        public async Task<BaseResult<ExpenditureDto>> GetByIdAsync(short id)
        {
            Expenditure expenditure;
            try
            {
                expenditure = await _expenditureRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);

                var result = _expenditureValidator.ValidateOrNull(expenditure);

                if (!result.IsSuccess)
                    return new BaseResult<ExpenditureDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ExpenditureDto>()
                {
                    ErrorMessage = ErrorMessage.ExpenditureNotFound,
                    ErrorCode = (int)ErrorCodes.ExpenditureNotFound
                };
            }

            return new BaseResult<ExpenditureDto>()
            {
                Data = _mapper.Map<ExpenditureDto>(expenditure),
                
            };
        }
    }
}
