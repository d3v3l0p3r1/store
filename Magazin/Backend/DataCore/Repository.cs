using BaseCore.DAL.Abstractions;
using BaseCore.DAL.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCore.DAL.Implementations
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        protected readonly DataContext _dataContext;
        public Repository(DataContext context)
        {
            _dataContext = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var dbSet = _dataContext.Set<TEntity>();
            await dbSet.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dbSet = _dataContext.Set<TEntity>();
            dbSet.Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }


        public virtual IQueryable<TEntity> GetAllAsNotracking(bool hidden = false)
        {
            var dbSet = _dataContext.Set<TEntity>().Where(x => x.Hidden == hidden).AsNoTracking();
            return dbSet;
        }

        public virtual IQueryable<TEntity> GetAll(bool hidden = false)
        {
            return _dataContext.Set<TEntity>().Where(x => x.Hidden == hidden);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            entity.Hidden = true;
            _dataContext.Set<TEntity>().Update(entity);
            return _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            _dataContext.Set<TEntity>().RemoveRange(entities);
            return _dataContext.SaveChangesAsync();
        }

        public virtual Task<TEntity> GetAsync(TKey id)
        {
            //return _dataContext.Set<TEntity>().FirstOrDefaultAsync(x => (TKey)x.Id == (TKey)id); // TODO : fix it
            throw new System.Exception();
        }
    }
}
