using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions;
using BaseCore.DAL.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _dataContext;
        public Repository(DataContext context)
        {
            _dataContext = context;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var dbSet = _dataContext.Set<T>();
            await dbSet.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var dbSet = _dataContext.Set<T>();
            dbSet.Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }


        public virtual IQueryable<T> GetAllAsNotracking(bool hidden = false)
        {
            var dbSet = _dataContext.Set<T>().Where(x=> x.Hidden == hidden).AsNoTracking();
            return dbSet;
        }

        public virtual IQueryable<T> GetAll(bool hidden = false)
        {
            return _dataContext.Set<T>().Where(x=>x.Hidden == hidden);
        }

        public virtual Task DeleteAsync(T entity)
        {
            entity.Hidden = true;
            _dataContext.Set<T>().Update(entity);
            return _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync(IEnumerable<T> entities)
        {            
            _dataContext.Set<T>().RemoveRange(entities);
            return _dataContext.SaveChangesAsync();
        }

        public virtual Task<T> GetAsync(long id)
        {
            return _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public class Repository : IRepository
    {
        protected readonly DataContext _dataContext;
        public Repository(DataContext context)
        {
            _dataContext = context;
        }

        public virtual async Task<T> CreateAsync<T>(T entity) where T : BaseEntity
        {
            var dbSet = _dataContext.Set<T>();
            await dbSet.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            var dbSet = _dataContext.Set<T>();
            dbSet.Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }


        public virtual IQueryable<T> GetAllAsNotracking<T>(bool hidden = false) where T : BaseEntity
        {
            var dbSet = _dataContext.Set<T>().Where(x => x.Hidden == hidden).AsNoTracking();
            return dbSet;
        }

        public virtual IQueryable<T> GetAll<T>(bool hidden = false) where T : BaseEntity
        {
            return _dataContext.Set<T>().Where(x => x.Hidden == hidden);
        }

        public virtual Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            entity.Hidden = true;
            _dataContext.Set<T>().Update(entity);
            return _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _dataContext.Set<T>().RemoveRange(entities);
            return _dataContext.SaveChangesAsync();
        }

        public virtual Task<T> GetAsync<T>(long id) where T : BaseEntity
        {
            return _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
