﻿using System;
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

        public virtual Task<T> FindAsync(int id)
        {
            return _repository.FindAsync(id);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await FindAsync(id);
            await _repository.DeleteAsync(entity);
        }
    }
}