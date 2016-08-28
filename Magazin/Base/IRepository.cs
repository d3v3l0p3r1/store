using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        T Find(int id);
        IQueryable<T> GetAll();
    }
}
