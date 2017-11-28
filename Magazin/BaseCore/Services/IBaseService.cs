using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        Task<IQueryable<T>> GetAllAsync(IUnitOfWork uow);
        Task<T> FindAsync(IUnitOfWork uow, int id);
        Task<T> UpdateAsync(IUnitOfWork uow, T entity);
        Task DeleteAsync(IUnitOfWork uow, int id);
    }
}
