using WebApi.Application.Resources;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Validations
{
    public class TargetValidator : ITargetValidator

    {
        public BaseResult ValidateOrNull(Target model)
        {
            if (model == null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TargetNotFound,
                    ErrorCode = (int)ErrorCodes.TargetNotFound,
                };
            return new BaseResult();
        }

        public BaseResult CreateValidator(Target model)
        {
            if (model != null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TargetAlreadyExists,
                    ErrorCode = (int)ErrorCodes.TargetAlreadyExists,
                };
            return new BaseResult();
        }
    }
}
