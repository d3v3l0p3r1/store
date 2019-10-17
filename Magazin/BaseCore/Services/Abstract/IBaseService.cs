using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> FindAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> CreateAsync(T entity);
    }
}
