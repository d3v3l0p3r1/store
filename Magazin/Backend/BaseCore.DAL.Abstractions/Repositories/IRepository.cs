using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCore.DAL.Abstractions.Repositories
{
    public interface IRepository
    {
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task<T> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<T> GetAsync<T>(long id) where T : BaseEntity;
        IQueryable<T> GetAllAsNotracking<T>(bool hidden = false) where T : BaseEntity;
        IQueryable<T> GetAll<T>(bool hidden = false) where T : BaseEntity;
    }


    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<T> GetAsync(long id);
        IQueryable<T> GetAllAsNotracking(bool hidden = false);
        IQueryable<T> GetAll(bool hidden = false);
    }
}
