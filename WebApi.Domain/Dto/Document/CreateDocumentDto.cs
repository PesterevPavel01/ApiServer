using WebApi.Domain.Entity;

namespace WebApi.Domain.Dto.Document
{
    public record CreateDocumentDto(String Name,double Value,string Comment,short ExpenditureId,short OrganizationId, short DepartmentId );

}
