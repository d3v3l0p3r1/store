using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Services.Abstract
{  

    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        T Find(int id);
        IQueryable<T> GetAll();
        DbSet<T> GetDbSet();
    }
}
