using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;

namespace BaseCore.Products.Implementations.Services
{
    public class KindService : IKindService
    {
        private readonly IRepository<ProductKind> _repository;
        public KindService(IRepository<ProductKind> repository)
        {
            _repository = repository;
        }

        public IQueryable<ProductKind> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public Task CreateAsync(ProductKind kind)
        {
            return _repository.CreateAsync(kind);
        }
    }
}
