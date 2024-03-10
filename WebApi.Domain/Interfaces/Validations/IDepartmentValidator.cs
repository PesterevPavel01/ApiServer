using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface IDepartmentValidator:IBaseValidator<Department>
    {
        /// <summary>
        /// Проверяем наличие подразделения в БД, если есть, то создать нельзя
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Department department);
    }
}
