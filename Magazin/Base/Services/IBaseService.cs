using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DAL;

namespace Base.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
    }

    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }
        

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
