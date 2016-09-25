using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Base;
using Base.DAL;
using Base.Entities;
using Data.DAL;
using RefactorThis.GraphDiff;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly DataContext _dataContext;
        private readonly IUpdateConfigurationManager _manager;

        public Repository(DataContext context)
        {
            _dataContext = context;
            _manager = UpdateConfiguration.GetConfigurationManager();
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
            var updateConfig = _manager.GetConfiguration<T>();

            if (updateConfig != null)                
            {
                _dataContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Detached;
                _dataContext.UpdateGraph(entity, updateConfig);
            }
            else
            {
                var dbSet = _dataContext.Set<T>();                
                dbSet.Attach(entity);
                _dataContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            }
            
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

        public void SaveChanges()
        {
            _dataContext.SaveChanges();            
        }

        public T UpdateGraph(T entity, Expression<Func<IUpdateConfiguration<T>, object>> expr)
        {
            return _dataContext.UpdateGraph(entity, expr);
        }


    }
}
