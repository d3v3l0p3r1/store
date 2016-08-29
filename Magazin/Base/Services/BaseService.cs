using System.Linq;
using Base.DAL;
using Base.Entities;

namespace Base.Services
{
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