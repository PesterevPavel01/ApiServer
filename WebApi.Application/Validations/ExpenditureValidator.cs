using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Resources;
using WebApi.Domain.Entity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interfaces.Validations;
using WebApi.Domain.Result;

namespace WebApi.Application.Validations
{
    public class ExpenditureValidator:IExpenditureValidator
    {
        public BaseResult ValidateOrNull(Expenditure model)
        {
            if (model == null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ExpenditureNotFound,
                    ErrorCode = (int)ErrorCodes.ExpenditureNotFound,
                };
            return new BaseResult();
        }
        public BaseResult CreateValidator(Expenditure expenditure)
        {
            if (expenditure != null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ExpenditureAlreadyExists,
                    ErrorCode = (int)ErrorCodes.ExpenditureAlreadyExists,
                };
            return new BaseResult();
        }
    }
}
