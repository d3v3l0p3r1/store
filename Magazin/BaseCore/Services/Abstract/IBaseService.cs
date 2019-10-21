using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        IQueryable<T> GetAllAsNotracking();
        Task<T> GetAsync(long id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(long id);
        Task<T> CreateAsync(T entity);
    }
}
