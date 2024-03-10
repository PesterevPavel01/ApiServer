using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface IBaseValidator<in T> where T:class 
    {
        BaseResult ValidateOrNull(T model);
    }
}
