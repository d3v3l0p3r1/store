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

        public virtual IQueryable<T> GetQuery()
        {
            return _repository.GetAll();
        }

        public virtual IQueryable<T> GetAllAsNotracking()
        {
            var all = _repository.GetAllAsNotracking();
            return all.Where(x => x.Hidden == false);
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

        public virtual Task<T> GetAsync(long id)
        {
            return _repository.GetAsync(id);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await GetAsync(id);
            await _repository.DeleteAsync(entity);
        }
    }
}