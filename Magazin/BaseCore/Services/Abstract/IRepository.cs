using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{  

    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        T Find(int id);
        IQueryable<T> GetAll();

        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> FindAsync(int id);        
    }
}
