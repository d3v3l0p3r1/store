using System.Linq;
using Base.DAL;
using Base.Entities;

namespace Base.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {

        public IQueryable<T> GetAll(IUnitOfWork uow)
        {
            return uow.GetRepository<T>().GetAll()
                .Where(x => !x.Hidden);                        
        }
    }
}