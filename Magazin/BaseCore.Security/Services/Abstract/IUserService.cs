using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Services;

namespace BaseCore.Security.Services.Abstract
{
    public interface IUserService
    {
        Task DeleteAsync(IUnitOfWork uow, int id);
        Task<User> FindAsync(IUnitOfWork uow, int id);
        Task<IQueryable<User>> GetAllAsync(IUnitOfWork uow);
        Task<User> UpdateAsync(IUnitOfWork uow, User entity);
    }
}