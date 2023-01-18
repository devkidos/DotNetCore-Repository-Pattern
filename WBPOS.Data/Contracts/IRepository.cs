using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WBPOS.Data.Contracts
{
    public interface IRepository<T> //where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(string path = null);
        Task<IQueryable<T>> FindAll();
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        //Task<T> GetById(object id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);

        //   DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);
    }
}
