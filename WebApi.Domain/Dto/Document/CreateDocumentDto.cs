using WebApi.Domain.Entity;

namespace WebApi.Domain.Dto.Document
{
    public record CreateDocumentDto(double Value, DateTime Date,string Comment,string Expenditure,string Organization, string Department );

}
