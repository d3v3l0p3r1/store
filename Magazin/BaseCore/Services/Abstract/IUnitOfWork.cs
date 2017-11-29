using System;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, new();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
