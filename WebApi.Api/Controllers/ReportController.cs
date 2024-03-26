using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Order;
using WebApi.Domain.Dto.Report;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReportController : ControllerBase
    {  
        private readonly IDocumentService _documentService;
        public ReportController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("ReportController работает!");
        }

        [HttpGet("Expenditures")]
        public async Task<ActionResult<CollectionResult<ExpenseReportDto>>> GetDocumentById(string user, [FromBody] ReportArgument argument)
        {
            var response = await _documentService.GetReportAsync(argument);

            return Ok(response);
        }
    }
}
