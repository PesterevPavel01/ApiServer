using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        private readonly ICommonService<ExpenditureDto, short> _expenditureService;
        public ExpenditureController(ICommonService<ExpenditureDto, short> expenditureService)
        {
            _expenditureService = expenditureService;
        }

        [HttpGet("test")]
        public IActionResult TestController()
        {
            return Ok("ExpenditureController работает!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> GetExpenditureById(short id)
        {
            var response = await _expenditureService.GetByIdAsync(id);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet()]
        public async Task<ActionResult<CollectionResult<ExpenditureDto>>> GetExpenditure()
        {

            var response = await _expenditureService.GetAllAsync();

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> CreateExpenditure([FromBody] ExpenditureDto expenditure)
        {
            if (expenditure != null)
            {
                var response = await _expenditureService.CreateAsync(expenditure);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> UpdateExpenditure([FromBody] ExpenditureDto expenditure)
        {
            if (expenditure != null)
            {
                var response = await _expenditureService.UpdateAsync(expenditure);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> DeleteExpenditure(short id)
        {
            var response = await _expenditureService.DeleteAsync(id);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }
    }
}
