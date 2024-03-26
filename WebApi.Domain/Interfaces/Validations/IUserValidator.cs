using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface IUserValidator : IBaseValidator<User>
    {
        /// <summary>
        /// Проверяем наличие пользователя в БД, если есть, то создать нельзя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        BaseResult CreateValidator(User user);
    }
}
