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

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("get")]
        public async Task<ActionResult<CollectionResult<DocumentDto>>> GetDocuments([FromBody] List<DateTime> dates)
        {

            var response = await _documentService.GetDocumentsAsync(dates[0], dates[1]);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("{expenditure}")]
        public async Task<ActionResult<CollectionResult<DocumentDto>>> GetDocuments(string expenditure,[FromBody] List<DateTime> dates)
        {
            var response = await _documentService.GetDocumentsByExpenditureAsync(dates[0], dates[1], expenditure);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<ActionResult<BaseResult<CreateDocumentDto>>> CreateDocument ([FromBody] CreateDocumentDto document)
        {
            if (document != null)
            {
                var response = await _documentService.CreateDocumentAsync(document);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpPatch()]
        public async Task<ActionResult<BaseResult<CreateDocumentDto>>> UpdateDocument([FromBody] DocumentDto document)
        {
            if (document != null)
            {
                var response = await _documentService.UpdateDocumentAsync(document);

                if (response.IsSuccess) return Ok(response);

                return BadRequest(response);
            }
            return BadRequest();
        }

        [HttpDelete()]
        public async Task<ActionResult<BaseResult<DocumentDto>>> DeleteDocument([FromBody] long id)
        {
            var response = await _documentService.DeleteDocumentAsync(id);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }
    }
}
