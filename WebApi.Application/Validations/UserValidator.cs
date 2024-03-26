using WebApi.Application.Resources;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Validations
{
    public class UserValidator : IUserValidator
    {
        public BaseResult ValidateOrNull (User model)
        {
            if (model == null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound,
                };
            return new BaseResult();
        }

        public BaseResult CreateValidator(User model)
        {
            if (model != null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists,
                };
            return new BaseResult();
        }
    }
}
