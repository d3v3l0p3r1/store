using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        IQueryable<T> GetAll(IUnitOfWork uow);
        Task<T> FindAsync(IUnitOfWork uow, int id);
        Task<T> UpdateAsync(IUnitOfWork uow, T entity);
        Task DeleteAsync(IUnitOfWork uow, int id);
    }
}
