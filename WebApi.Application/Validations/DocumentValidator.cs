using WebApi.Application.Resources;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Validations
{
    public class DocumentValidator:IDocumentValidator
    {
        public BaseResult ValidateOrNull(Document model)
        {
            if (model == null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.DepartmentNotFound,
                    ErrorCode = (int)ErrorCodes.DepartmentNotFound,
                };
            return new BaseResult();
        }
        public BaseResult CreateValidator(Document document)
        {
            if (document != null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.DocumentAlreadyExists,
                    ErrorCode = (int)ErrorCodes.DocumentAlreadyExists,
                };
            return new BaseResult();
        }

    }
}
