using System;
using System.Collections.Generic;
using Base.DAL;
using Base.Entities;
using Base.Services;

namespace Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork()
        {
            _repositories = new Dictionary<Type, object>();
            _dataContext = new DataContext();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories.Add(typeof(T), new Repository<T>(_dataContext));
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            _repositories.Clear();
        }
    }
}
