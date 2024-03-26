using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Services;
using WebApi.Application.Resources;
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

            return Ok(response);
        }

        [HttpGet()]
        public async Task<ActionResult<CollectionResult<OrganizationDto>>> GetOrganization()
        {

            var response = await _organizationService.GetAllAsync();

            return Ok(response);

        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<OrganizationDto>>> CreateOrganization([FromBody] OrganizationDto organization)
        {
                var response = await _organizationService.CreateAsync(organization);

                return Ok(response);
        }

        [HttpPost("all")]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> CreateMultipleOrganizations([FromBody] List<OrganizationDto> organizations)
        {

                var response = await _organizationService.CreateMultiple(organizations);

                return Ok(response);
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<OrganizationDto>>> UpdateOrganization([FromBody] OrganizationDto organization)
        {
                var response = await _organizationService.UpdateAsync(organization);

                return Ok(response);
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<OrganizationDto>>> DeleteOrganization([FromBody] short id)
        {
            var response = await _organizationService.DeleteAsync(id);

            return Ok(response);
        }
    }
}
