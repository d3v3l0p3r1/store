using System.Data.Entity.Migrations;
using System.Linq;
using Base;
using Base.DAL;
using Base.Entities;
using Data.DAL;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        public Repository()
        {
            _dataContext = new DataContext();
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
            dbSet.AddOrUpdate(entity);
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
    }
}
