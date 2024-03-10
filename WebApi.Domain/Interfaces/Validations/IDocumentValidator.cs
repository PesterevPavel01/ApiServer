using WebApi.Domain.Entity;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface IDocumentValidator:IBaseValidator<Document>
    {
        BaseResult CreateValidator(Document document);
    }
}
