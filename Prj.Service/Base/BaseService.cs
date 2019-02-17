using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Prj.DataAccess.Context;
using Prj.Utilities;

namespace Prj.Services.Base
{
    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<TEntity> _entities;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _entities = _unitOfWork.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _unitOfWork.Entry(entity).State = EntityState.Added;
        }

        public void Edit(TEntity entity)
        {
            _unitOfWork.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _unitOfWork.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<TEntity> FindByIdAsync(TKey key, bool asNoTracking = true, params string[] includes)
        {
            var query = _entities
                    .AsQueryable();

            if (key.GetType() != typeof(string))
                query = query.Where("Id = " + key);
            else
                query = query.Where("Id = " + '"' + key + '"');

            if (asNoTracking)
                query.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, params string[] includes)
        {
            var query = _entities
                .Where(predicate)
                .AsQueryable();

            if (asNoTracking)
                query.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await _entities.CountAsync(predicate);
            return await _entities.CountAsync();
        }

        public async Task<List<T>> GetListAsync<T>(Expression<Func<TEntity, bool>> predicate = null,
                                                        string orderBy = null,
                                                        Pager pager = null,
                                                        bool desc = false,
                                                        bool asNoTracking = true,
                                                        params string[] includs)
        {
            var models = _entities
                .AsQueryable();

            if(asNoTracking)
                models = models.AsNoTracking();

            if (includs != null)
                foreach (var item in includs)
                    models.Include(item);

            if (predicate != null)
                models = models.Where(predicate);

            if (orderBy != null && desc)
                models = models.OrderBy(orderBy + " desc");
            else if (orderBy != null && !desc)
                models = models.OrderBy(orderBy + " asc");

            if (pager != null && orderBy != null)
            {
                models = models
                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize)
                    .AsQueryable();
            }
            else if (pager != null && orderBy == null)
            {
                models = models
                    .OrderBy("Id asc")
                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize)
                    .AsQueryable();
            }

            return await models.ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                        string orderBy = null,
                                                        Pager pager = null,
                                                        bool desc = false,
                                                        bool asNoTracking = true,
                                                        params string[] includs)
        {
            var models = _entities
                .AsQueryable();

            if (asNoTracking)
                models = models.AsNoTracking();

            if (includs != null)
                foreach (var item in includs)
                    models.Include(item);

            if (predicate != null)
                models = models.Where(predicate);

            if (orderBy != null && desc)
                models = models.OrderBy(orderBy + " desc");
            else if (orderBy != null && !desc)
                models = models.OrderBy(orderBy + " asc");

            if (pager != null && orderBy != null)
            {
                models = models
                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize)
                    .AsQueryable();
            }
            else if (pager != null && orderBy == null)
            {
                models = models
                    .OrderBy("Id asc")
                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize)
                    .AsQueryable();
            }

            return await models.ToListAsync();
        }
    }
}