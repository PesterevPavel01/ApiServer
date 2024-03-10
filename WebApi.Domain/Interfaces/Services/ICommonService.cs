using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Services
{
    public interface ICommonService<T,Id>
    {
        Task<CollectionResult<T>> GetAllAsync();
        Task<BaseResult<T>> GetByIdAsync(Id id);
        Task<BaseResult<T>> CreateAsync(T model);
        Task<BaseResult<T>> UpdateAsync(T model);
        Task<BaseResult<T>> DeleteAsync(Id id);
    }
}
