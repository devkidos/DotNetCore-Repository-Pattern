using WBPOS.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WBPOS.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class //: BaseEntity
    {
        protected WBPOSContext RepositoryContext { get; set; }
        public Repository(WBPOSContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public async Task<T> Delete(T entity)
        {
            return await Task.Run(() =>
            {
                this.RepositoryContext.Set<T>().Remove(entity);
                this.RepositoryContext.SaveChangesAsync();
                return entity;
            });
        }

        public async Task<IQueryable<T>> FindAll()
        {
            return await Task.Run(() =>
            {
                var data = this.RepositoryContext.Set<T>().AsNoTracking();
                return data;
            });
        }

        public IQueryable<T> GetAll()
        {
            
            var data = this.RepositoryContext.Set<T>().AsNoTracking();
            return data;
            
        }

        public IQueryable<T> GetAll(string path = null)
        {
            var model = RepositoryContext.Set<T>();
            if (!string.IsNullOrEmpty(path))
            {
                this.RepositoryContext.Entry(model).Reference(path).Load();
            }
            return model;

        }
        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                var data = this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
                return data;
            });
        }

        public async Task<T> Insert(T entity)
        {
            await this.RepositoryContext.Set<T>().AddAsync(entity);
            await this.RepositoryContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            return await Task.Run(() =>
            {
                this.RepositoryContext.Set<T>().Update(entity);
                this.RepositoryContext.SaveChangesAsync();
                return entity;
            });
        }

    }
}
