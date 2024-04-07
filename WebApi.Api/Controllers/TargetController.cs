using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Target;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly ICommonService<TargetDto, long> _targetService;
        public TargetController(ICommonService<TargetDto, long> targetService)
        {
            _targetService = targetService;
        }

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("TargetController работает!");
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<TargetDto>>> CreateTarget([FromBody] TargetDto target)
        {
            var response = await _targetService.CreateAsync(target);

            return Ok(response);
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<TargetDto>>> UpdateTarget([FromBody] TargetDto target)
        {
            var response = await _targetService.UpdateAsync(target);

            return Ok(response);
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<TargetDto>>> DeleteTarget([FromBody] long id)
        {
            var response = await _targetService.DeleteAsync(id);

            return Ok(response);
        }

    }
}
