using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services.Abstract;

namespace BaseCore.Services.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }


        public virtual IQueryable<T> GetAll()
        {
            var all = _repository.GetAll();
            return all.Where(x => !x.Hidden);
        }

        public virtual T Find(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return _repository.Find(id);
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            PrepareEntity(entity);

            var result = _repository.Update(entity);

            return result;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            PrepareEntity(entity);

            var result = await _repository.UpdateAsync(entity);

            return result;
        }

        public virtual void Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException();
            }

            var entity = _repository.Find(id);

            entity.Hidden = true;

            _repository.Update(entity);
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            return _repository.Create(entity);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            return await _repository.CreateAsync(entity);
        }

        protected virtual void PrepareEntity(T entity)
        {

        }
    }
}