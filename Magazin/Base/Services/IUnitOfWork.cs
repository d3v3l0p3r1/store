using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DAL;
using Base.Entities;

namespace Base.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T: BaseEntity;
        void SaveChanges();
    }
}
