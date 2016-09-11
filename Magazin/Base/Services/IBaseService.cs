using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;

namespace Base.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(IUnitOfWork uow);
        T Find(IUnitOfWork uow, int id);
        T Update(IUnitOfWork uow, T entity);
        void Delete(IUnitOfWork uow, int id);
    }
}
