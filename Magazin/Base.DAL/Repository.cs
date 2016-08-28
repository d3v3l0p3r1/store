using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL
{
    public sealed class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDataContext _dataContext;
        public Repository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public T Create(T entity)
        {
            var dbSet = _dataContext.GetDbSet<T>();
            dbSet.Add(entity);            
            return entity;
        }

        public void Delete(T entity)
        {
            var dbSet = _dataContext.GetDbSet<T>();
            dbSet.Remove(entity);
        }

        public T Update(T entity)
        {
            var dbSet = _dataContext.GetDbSet<T>();
            dbSet.AddOrUpdate(entity);
            return entity;
        }

        public T Find(int id)
        {
            var dbSet = _dataContext.GetDbSet<T>();
            return dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            var dbSet = _dataContext.GetDbSet<T>();
            return dbSet;
        }
    }
}
