using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseDocumentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public ExpenseDocumentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> GetDepartment(short id)
        {
            var response = await _departmentService.GetDepartmentByIdAsync(id);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Сервер работает!");
        }
    }
}
