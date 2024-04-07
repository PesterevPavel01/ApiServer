using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Resources;
using WebApi.Application.Validations;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Target;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Repositories;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Services
{
    internal class TargetService : ICommonService<TargetDto, long>
    {
        private readonly IBaseRepository<Target> _targetRepository;
        private readonly IBaseRepository<Expenditure> _expenditureRepository;
        private readonly ITargetValidator _targetValidator;
        private readonly IExpenditureValidator _expenditureValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TargetService(IBaseRepository<Target> targetRepository,
            IBaseRepository<Expenditure> expenditureRepository, 
            ITargetValidator targetValidator,
            IExpenditureValidator expenditureValidator,
            IMapper mapper,
            ILogger logger
            )
        {
            _targetRepository = targetRepository;
            _expenditureRepository = expenditureRepository;
            _mapper = mapper;
            _logger = logger;
            _targetValidator = targetValidator;
            _expenditureValidator = expenditureValidator;
        }

        public async Task<BaseResult<TargetDto>> CreateAsync(TargetDto model)
        {
            if (model == null) return new BaseResult<TargetDto>() { ErrorMessage = ErrorMessage.IncorrectInputObject, ErrorCode = (int)ErrorCodes.IncorrectInputObject };
            
            try
            {
                var expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Expenditure);
                var result = _expenditureValidator.ValidateOrNull(expenditure);

                if (!result.IsSuccess)
                    return new BaseResult<TargetDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                var target = await _targetRepository.GetAll().FirstOrDefaultAsync(x => x.ExpenditureID == expenditure.Id);
                result = _targetValidator.CreateValidator(target);

                if (!result.IsSuccess)
                    return new BaseResult<TargetDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                target = new Target
                {
                    ExpenditureID = expenditure.Id,
                    Month = model.Month,
                    Year = model.Year,
                    Value = model.Value,
                };

                target=await _targetRepository.CreateAsync(target);

                return new BaseResult<TargetDto>()
                {
                    Data = new TargetDto(target.Id, expenditure.Name,target.Value,target.Month, target.Year)
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TargetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<TargetDto>> UpdateAsync(TargetDto model)
        {
            if (model == null) return new BaseResult<TargetDto>() { ErrorMessage = ErrorMessage.IncorrectInputObject, ErrorCode = (int)ErrorCodes.IncorrectInputObject };
            try
            {
                var target = await _targetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                var result = _targetValidator.ValidateOrNull(target);

                if (!result.IsSuccess) //Если нет такой записи, то создаем новую
                {
                    var resultModel= await CreateAsync(model);
                    return resultModel;
                }
                else 
                {
                    target.Value = model.Value;
                    await _targetRepository.UpdateAsync(target);
                }

                return new BaseResult<TargetDto>()
                {
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TargetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<TargetDto>> GetAsync(TargetDto model)
        {
            Target target;
            Expenditure expenditure;
            try
            {
                expenditure = await _expenditureRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Expenditure);
                var result = _expenditureValidator.ValidateOrNull(expenditure);

                if (!result.IsSuccess)
                    return new BaseResult<TargetDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                target = await _targetRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.ExpenditureID == expenditure.Id && x.Month==model.Month && x.Year==model.Year);

                result = _targetValidator.ValidateOrNull(target);

                if (!result.IsSuccess)
                    return new BaseResult<TargetDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TargetDto>()
                {
                    ErrorMessage = ErrorMessage.TargetNotFound,
                    ErrorCode = (int)ErrorCodes.TargetNotFound
                };
            }

            return new BaseResult<TargetDto>()
            {
                Data = new TargetDto(target.Id, expenditure.Name,target.Value,target.Month,target.Year),
            };
        }

        public async Task<BaseResult<TargetDto>> DeleteAsync(long id)
        {
            try
            {
                var target = await _targetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _targetValidator.ValidateOrNull(target);

                if (!result.IsSuccess)
                    return new BaseResult<TargetDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                await _targetRepository.RemoveAsync(target);

                return new BaseResult<TargetDto>()
                {
                    Data = _mapper.Map<TargetDto>(target),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TargetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public Task<BaseResult<TargetDto>> CreateMultiple(List<TargetDto> listModel)
        {
            throw new NotImplementedException();
        }

        public Task<CollectionResult<TargetDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<TargetDto>> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
