using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services;

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
            return entity;
        }

        public T Find(int id)
        {
            var dbSet = _dataContext.Set<T>();
            return dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            var dbSet = _dataContext.Set<T>();
            return dbSet;
        }

        public Task<T> CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
