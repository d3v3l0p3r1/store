using System;
using System.Linq;
using Base.DAL;
using Base.Entities;

namespace Base.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        public virtual IQueryable<T> GetAll(IUnitOfWork uow)
        {
            return uow.GetRepository<T>().GetAll()
                .Where(x => !x.Hidden);
        }

        public virtual T Find(IUnitOfWork uow, int id)
        {
            if (uow == null)
            {
                throw new ArgumentNullException();
            }

            if (id == 0)
            {
                throw new ArgumentException();
            }

            return uow.GetRepository<T>().Find(id);
        }

        public virtual T Update(IUnitOfWork uow, T entity)
        {
            if (uow == null)
            {
                throw new ArgumentNullException();
            }

            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            var result = entity.Id != 0 
                ? uow.GetRepository<T>().Update(entity) 
                : uow.GetRepository<T>().Create(entity);

            uow.SaveChanges();

            return result;
        }

        public virtual void Delete(IUnitOfWork uow, int id)
        {
            if (id == 0)
            {
                throw new ArgumentException();
            }

            var entity = uow.GetRepository<T>().Find(id);

            entity.Hidden = true;

            uow.GetRepository<T>().Update(entity);

            uow.SaveChanges();
        }
    }
}