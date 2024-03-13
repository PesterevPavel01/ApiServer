using WebApi.Domain.Dto.Document;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Services
{
    public interface IDepartmentService
    {

        Task<CollectionResult<DepartmentDto>> GetDepartmentsAsync();
        Task<BaseResult<DepartmentDto>> GetDepartmentByIdAsync(short id);
        Task<BaseResult<DepartmentDto>> CreateDepartmentAsync(DepartmentDto model);
        Task<BaseResult<DepartmentDto>> CreateDepartmentsMultiple(List<DepartmentDto> listModel);
        Task<BaseResult<DepartmentDto>> UpdateDepartmentAsync(DepartmentDto model);
        Task<BaseResult<DepartmentDto>> DeleteDepartmentAsync(short id);

    }
}
