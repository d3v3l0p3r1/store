using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        IQueryable<T> GetAll();
        T Find(int id);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(int id);
        T Create(T entity);
        Task<T> CreateAsync(T entity);
    }
}
