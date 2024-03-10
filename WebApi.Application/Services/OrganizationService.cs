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
    public class OrganizationService : ICommonService<OrganizationDto, short>
    {
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrganizationService(IBaseRepository<Organization> organizationRepository, IOrganizationValidator organizationValidator, IMapper mapper, ILogger logger)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _logger = logger;
            _organizationRepository = organizationRepository;

        }

        public async Task<BaseResult<OrganizationDto>> CreateAsync(OrganizationDto model)
        {
            try
            {
                var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                var result = _organizationValidator.CreateValidator(organization);

                if (!result.IsSuccess)
                    return new BaseResult<OrganizationDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                organization = new Organization
                {
                    Name = model.Name
                };

                await _organizationRepository.CreateAsync(organization);

                return new BaseResult<OrganizationDto>()
                {
                    Data = _mapper.Map<OrganizationDto>(organization),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<OrganizationDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<OrganizationDto>> UpdateAsync(OrganizationDto model)
        {
            try
            {
                var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                var result = _organizationValidator.ValidateOrNull(organization);

                if (!result.IsSuccess)
                    return new BaseResult<OrganizationDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                organization.Name = model.Name;

                await _organizationRepository.UpdateAsync(organization);

                return new BaseResult<OrganizationDto>()
                {
                    Data = _mapper.Map<OrganizationDto>(organization),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<OrganizationDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<OrganizationDto>> DeleteAsync(short id)
        {
            try
            {
                var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _organizationValidator.ValidateOrNull(organization);

                if (!result.IsSuccess)
                    return new BaseResult<OrganizationDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                await _organizationRepository.RemoveAsync(organization);

                return new BaseResult<OrganizationDto>()
                {
                    Data = _mapper.Map<OrganizationDto>(organization),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<OrganizationDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<CollectionResult<OrganizationDto>> GetAllAsync()
        {
            OrganizationDto[] organizations;
            try
            {
                organizations = await _organizationRepository.GetAll()
                    .Select(x => new OrganizationDto(x.Id, x.Name)).ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<OrganizationDto>()
                {
                    ErrorMessage = ErrorMessage.OrganizationNotFound,
                    ErrorCode = (int)ErrorCodes.OrganizationNotFound
                };
            }

            return new CollectionResult<OrganizationDto>()
            {
                Data = organizations,
                Count = organizations.Length
            };
        }

        public async Task<BaseResult<OrganizationDto>> GetByIdAsync(short id)
        {
            OrganizationDto organization;
            try
            {
                organization = await _organizationRepository.GetAll()
                    .Select(x => new OrganizationDto(x.Id, x.Name))
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<OrganizationDto>()
                {
                    ErrorMessage = ErrorMessage.OrganizationNotFound,
                    ErrorCode = (int)ErrorCodes.OrganizationsNotFound
                };
            }

            return new BaseResult<OrganizationDto>()
            {
                Data = organization
            };
        }
    }
}
