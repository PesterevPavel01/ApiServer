using WebApi.Domain.Entity;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Validations
{
    public interface ITargetValidator:IBaseValidator<Target>
    {
        /// <summary>
        /// Проверяем наличие цели в БД, если есть, то создать нельзя
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Target target);
    }
}
