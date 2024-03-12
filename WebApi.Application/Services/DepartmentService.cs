using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using WebApi.Application.Resources;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Repositories;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseRepository<Department> _departmentRepository;
        private readonly IDepartmentValidator _departmentValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DepartmentService(IBaseRepository<Department> departmentRepository,IDepartmentValidator departmentValidator,IMapper mapper, ILogger logger)
        {
            _departmentRepository = departmentRepository;
            _departmentValidator = departmentValidator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResult<DepartmentDto>> CreateDepartmentAsync(DepartmentDto model)
        {
            try
            {
                var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                var result = _departmentValidator.CreateValidator(department);

                if (!result.IsSuccess)
                    return new BaseResult<DepartmentDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                department=new Department
                {
                    Name=model.Name 
                };

                await _departmentRepository.CreateAsync(department);

                return new BaseResult<DepartmentDto>()
                {
                    Data = _mapper.Map<DepartmentDto>(department),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DepartmentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<DepartmentDto>> UpdateDepartmentAsync(DepartmentDto model)
        {
            try
            {
                var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                var result = _departmentValidator.ValidateOrNull(department);

                if (!result.IsSuccess)
                    return new BaseResult<DepartmentDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                department.Name = model.Name;

                await _departmentRepository.UpdateAsync(department);

                return new BaseResult<DepartmentDto>()
                {
                    Data = _mapper.Map<DepartmentDto>(department),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DepartmentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<DepartmentDto>> DeleteDepartmentAsync(short id)
        {
            try
            {
                var department = await _departmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _departmentValidator.ValidateOrNull(department);

                if (!result.IsSuccess)
                    return new BaseResult<DepartmentDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };

                await _departmentRepository.RemoveAsync(department);

                return new BaseResult<DepartmentDto>()
                {
                    Data = _mapper.Map<DepartmentDto>(department),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DepartmentDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<CollectionResult<DepartmentDto>> GetDepartmentsAsync()
        {
            DepartmentDto[] departments;
            try 
            {
                departments=await _departmentRepository.GetAll()
                    .Select(x => new DepartmentDto(x.Id,x.Name)).ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<DepartmentDto>()
                {
                    ErrorMessage = ErrorMessage.DepartmentsNotFound,
                    ErrorCode = (int)ErrorCodes.DepartmentsNotFound
                };
            }

            return new CollectionResult<DepartmentDto>()
            {
                Data = departments,
                Count = departments.Length
            };
        }

        public async Task<BaseResult<DepartmentDto>> GetDepartmentByIdAsync(short id)
        {
            Department department;
            try
            {
                department = await _departmentRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _departmentValidator.ValidateOrNull(department);

                if (!result.IsSuccess)
                    return new BaseResult<DepartmentDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<DepartmentDto>()
                {
                    ErrorMessage = ErrorMessage.DepartmentNotFound,
                    ErrorCode = (int)ErrorCodes.DepartmentNotFound
                };
            }

            return new BaseResult<DepartmentDto>()
            {
                Data =  _mapper.Map<DepartmentDto>(department),
            };
        }

    }
}
