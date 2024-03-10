using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface IOrganizationValidator:IBaseValidator<Organization>
    {
        /// <summary>
        /// Проверяем наличие организации в БД, если есть, то создать нельзя
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Organization organization);
    }
}
