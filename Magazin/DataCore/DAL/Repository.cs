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
        private readonly DataContext _dataContext;
        public Repository(DataContext context)
        {
            _dataContext = context;
        }

        public T Create(T entity)
        {
            var dbSet = _dataContext.Set<T>();
            dbSet.Add(entity);
            _dataContext.SaveChanges();            
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var dbSet = _dataContext.Set<T>();
            await dbSet.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity)
        {
            var dbSet = _dataContext.Set<T>();
            dbSet.Remove(entity);
        }

        public T Update(T entity)
        {
            var dbSet = _dataContext.Set<T>();
            dbSet.Update(entity);
            _dataContext.SaveChanges();
            return entity;
        }        
        public async Task<T> UpdateAsync(T entity)
        {
            var dbSet = _dataContext.Set<T>();            
            dbSet.Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public T Find(int id)
        {
            var dbSet = _dataContext.Set<T>();
            return dbSet.Single(x=> x.Id == id);
        }

        public IQueryable<T> GetAll()
        {            
            var dbSet = _dataContext.Set<T>().AsNoTracking();
            return dbSet;
        }

        public DbSet<T> GetDbSet()
        {
            return _dataContext.Set<T>();
        }

    }
}
