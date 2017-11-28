using System;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T: BaseEntity;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
