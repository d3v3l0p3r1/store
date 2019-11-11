using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services;
using BaseCore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataCore.DAL
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


        public virtual IQueryable<T> GetAllAsNotracking()
        {
            var dbSet = _dataContext.Set<T>().AsNoTracking();
            return dbSet;
        }

        public virtual DbSet<T> GetDbSet()
        {
            return _dataContext.Set<T>();
        }

        public virtual Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
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
}
