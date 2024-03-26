using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Services;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Result;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseDocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public ExpenseDocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Сервер работает!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<DocumentDto>>> GetDocumentById(long id)
        {
            var response = await _documentService.GetDocumentByIdAsync(id);

            return Ok(response);
        }

        [HttpPost("get")]
        public async Task<ActionResult<CollectionResult<DocumentDto>>> GetDocuments([FromBody] List<DateTime> dates)
        {

            var response = await _documentService.GetDocumentsAsync(dates[0], dates[1]);

            return Ok(response);

        }

        [HttpPost("{expenditure}")]
        public async Task<ActionResult<CollectionResult<DocumentDto>>> GetDocuments(string expenditure,[FromBody] List<DateTime> dates)
        {
            var response = await _documentService.GetDocumentsByExpenditureAsync(dates[0], dates[1], expenditure);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<CreateDocumentDto>>> CreateDocument ([FromBody] CreateDocumentDto document)
        {
                var response = await _documentService.CreateDocumentAsync(document);

                return Ok(response);
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<CreateDocumentDto>>> UpdateDocument([FromBody] DocumentDto document)
        {
                var response = await _documentService.UpdateDocumentAsync(document);

                return Ok(response);
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<DocumentDto>>> DeleteDocument([FromBody] long id)
        {
            var response = await _documentService.DeleteDocumentAsync(id);

            return Ok(response);
        }
    }
}
