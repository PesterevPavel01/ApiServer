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
    public class DepartmentValidator : IDepartmentValidator
    {

        public BaseResult ValidateOrNull(Department model)
        {
            if (model == null)
                return new BaseResult()
                { 
                    ErrorMessage  =ErrorMessage.DepartmentNotFound,
                    ErrorCode=(int)ErrorCodes.DepartmentNotFound,
                };
            return new BaseResult();
        }
        public BaseResult CreateValidator(Department department)
        {
            if (department != null)
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.DepartmentAlreadyExists,
                    ErrorCode = (int)ErrorCodes.DepartmentAlreadyExists,
                };
            return new BaseResult();
        }

    }
}
