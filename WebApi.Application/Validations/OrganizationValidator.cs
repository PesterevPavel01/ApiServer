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
    public class OrganizationValidator:IOrganizationValidator
    {
        public BaseResult ValidateOrNull(Organization model)
        {
            if (model == null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.OrganizationNotFound,
                    ErrorCode = (int)ErrorCodes.OrganizationNotFound,
                };
            return new BaseResult();
        }
        public BaseResult CreateValidator(Organization organization)
        {
            if (organization != null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.OrganizationAlreadyExists,
                    ErrorCode = (int)ErrorCodes.OrganizationAlreadyExists,
                };
            return new BaseResult();
        }
    }
}
