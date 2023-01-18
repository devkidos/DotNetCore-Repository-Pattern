using DevKido.Utilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPOS.Services.Contracts
{
    public interface IService<T> where T : class
    {
        Task<ResultResponse<List<T>>> GetData();
        Task<ResultResponse<T>> GetDataById(object id);
        Task<ResultResponse<T>> Insert(T entity, string UserId = null);
        ////Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression); 

        Task<ResultResponse<T>> Update(T entity, string UserId = null);
        Task<ResultResponse<T>> Delete(object id);

    }
}
