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

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet()]
        public async Task<ActionResult<CollectionResult<DepartmentDto>>> GetDepartments()
        {

            var response = await _departmentService.GetDepartmentsAsync();

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> CreateDepartment([FromBody] DepartmentDto department)
        {
            if (department != null)
            {
                var response = await _departmentService.CreateDepartmentAsync(department);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpPost("all")]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> CreateMultipleDepartments([FromBody] List<DepartmentDto> departments)
        {
            if (departments != null)
            {
                var response = await _departmentService.CreateDepartmentsMultiple(departments);
                
                return response.IsSuccess ? Ok(response) : BadRequest(response);

            }
            return BadRequest();
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> UpdateDepartment([FromBody] DepartmentDto department)
        {
            if (department != null)
            {
                var response = await _departmentService.UpdateDepartmentAsync(department);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> DeleteDepartment([FromBody] short id)
        {
                var response = await _departmentService.DeleteDepartmentAsync(id);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
        }
    }
}
