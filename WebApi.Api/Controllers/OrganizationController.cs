using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Services;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrganizationController : Controller
    {
        private readonly ICommonService<OrganizationDto, short> _organizationService;
        public OrganizationController(ICommonService<OrganizationDto, short> organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("test")]
        public IActionResult TestController()
        {
            return Ok("OrganizationController работает!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<DepartmentDto>>> GetDocumentById(short id)
        {
            var response = await _organizationService.GetByIdAsync(id);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet()]
        public async Task<ActionResult<CollectionResult<OrganizationDto>>> GetOrganization()
        {

            var response = await _organizationService.GetAllAsync();

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<OrganizationDto>>> CreateOrganization([FromBody] OrganizationDto organization)
        {
            if (organization != null)
            {
                var response = await _organizationService.CreateAsync(organization);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpPost("all")]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> CreateMultipleOrganizations([FromBody] List<OrganizationDto> organizations)
        {
            if (organizations != null)
            {
                var response = await _organizationService.CreateMultiple(organizations);

                return response.IsSuccess ? Ok(response) : BadRequest(response);

            }
            return BadRequest();
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<OrganizationDto>>> UpdateOrganization([FromBody] OrganizationDto organization)
        {
            if (organization != null)
            {
                var response = await _organizationService.UpdateAsync(organization);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<OrganizationDto>>> DeleteOrganization([FromBody] short id)
        {
            var response = await _organizationService.DeleteAsync(id);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }
    }
}
