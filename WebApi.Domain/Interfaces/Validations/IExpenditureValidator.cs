using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface IExpenditureValidator: IBaseValidator<Expenditure>
    {
        /// <summary>
        /// Проверяем наличие статьи в БД, если есть, то создать нельзя
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Expenditure expenditure);
    }
}
