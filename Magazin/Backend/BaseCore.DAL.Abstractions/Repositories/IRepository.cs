using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCore.DAL.Abstractions.Repositories
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey: struct
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(TKey id);
        IQueryable<TEntity> GetAllAsNotracking(bool hidden = false);
        IQueryable<TEntity> GetAll(bool hidden = false);
    }
}
