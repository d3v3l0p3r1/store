using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Services.Abstract
{

    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> FindAsync(long id);
        IQueryable<T> GetAll();
        DbSet<T> GetDbSet();
    }
}
