using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services.Abstract;

namespace BaseCore.Services.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {

        public virtual IQueryable<T> GetAll(IUnitOfWork uow)
        {
            var rep = uow.GetRepository<T>();
            var all = rep.GetAll();
            return all.Where(x => !x.Hidden);
        }

        public virtual async Task<T> FindAsync(IUnitOfWork uow, int id)
        {
            if (uow == null)
            {
                throw new ArgumentNullException();
            }

            if (id == 0)
            {
                throw new ArgumentException();
            }

            var rep = uow.GetRepository<T>();

            return await rep.FindAsync(id);
        }

        public virtual async Task<T> UpdateAsync(IUnitOfWork uow, T entity)
        {
            if (uow == null)
            {
                throw new ArgumentNullException();
            }

            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            var rep = uow.GetRepository<T>();

            var result = await rep.UpdateAsync(entity);

            uow.SaveChanges();

            return result;
        }

        public virtual async Task DeleteAsync(IUnitOfWork uow, int id)
        {
            if (id == 0)
            {
                throw new ArgumentException();
            }

            var entity = await uow.GetRepository<T>().FindAsync(id);

            entity.Hidden = true;

            await uow.GetRepository<T>().UpdateAsync(entity);

            await uow.SaveChangesAsync();
        }
    }
}