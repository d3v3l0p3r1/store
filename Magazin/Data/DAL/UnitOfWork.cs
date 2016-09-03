using System.Collections.Generic;
using Base.DAL;
using Base.Entities;
using Base.Services;

namespace Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {                      
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>();
        }

        public void Dispose()
        {
            
        }
    }
}
