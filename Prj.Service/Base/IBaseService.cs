using Prj.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Prj.Services.Base
{
    public interface IBaseService<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity> FindByIdAsync(TKey key, bool asNoTracking = true, params string[] includes);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, params string[] includes);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<T>> GetListAsync<T>(Expression<Func<TEntity, bool>> predicate = null,
                                        string orderBy = null,
                                        Pager pager = null,
                                        bool desc = false,
                                        bool asNoTracking = true,
                                        params string[] includes);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                        string orderBy = null,
                                        Pager pager = null,
                                        bool desc = false,
                                        bool asNoTracking = true,
                                        params string[] includes);
    }
}
