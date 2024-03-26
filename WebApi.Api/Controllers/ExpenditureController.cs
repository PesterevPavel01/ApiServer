using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Services;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;
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

            return Ok(response);
        }

        [HttpGet()]
        public async Task<ActionResult<CollectionResult<ExpenditureDto>>> GetExpenditure()
        {

            var response = await _expenditureService.GetAllAsync();

            return Ok(response);
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> CreateExpenditure([FromBody] ExpenditureDto expenditure)
        {
                var response = await _expenditureService.CreateAsync(expenditure);

                return Ok(response);
        }

        [HttpPost("all")]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> CreateMultipleExpenditures([FromBody] List<ExpenditureDto> expenditures)
        {
                var response = await _expenditureService.CreateMultiple(expenditures);

                return  Ok(response);
          
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> UpdateExpenditure([FromBody] ExpenditureDto expenditure)
        {
                var response = await _expenditureService.UpdateAsync(expenditure);

                return Ok(response);
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<ExpenditureDto>>> DeleteExpenditure([FromBody] short id)
        {
            var response = await _expenditureService.DeleteAsync(id);

            return Ok(response);
        }
    }
}
