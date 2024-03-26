using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("test")]
        public IActionResult TestController()
        {
            return Ok("DepartmentController работает!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> GetDocumentById(short id)
        {
            var response = await _departmentService.GetDepartmentByIdAsync(id);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<ActionResult<CollectionResult<DepartmentDto>>> GetDepartments()
        {

            var response = await _departmentService.GetDepartmentsAsync();

            return Ok(response);

        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> CreateDepartment([FromBody] DepartmentDto department)
        {
                var response = await _departmentService.CreateDepartmentAsync(department);

                return Ok(response);
        }

        [HttpPost("all")]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> CreateMultipleDepartments([FromBody] List<DepartmentDto> departments)
        {
                var response = await _departmentService.CreateDepartmentsMultiple(departments);

            return Ok(response);

        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> UpdateDepartment([FromBody] DepartmentDto department)
        {

                var response = await _departmentService.UpdateDepartmentAsync(department);

                 return Ok(response);

        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> DeleteDepartment([FromBody] short id)
        {
                var response = await _departmentService.DeleteDepartmentAsync(id);

               return Ok(response);

        }
    }
}
