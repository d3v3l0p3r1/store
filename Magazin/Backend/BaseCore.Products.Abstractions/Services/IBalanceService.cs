using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Products.Abstractions.Models;

namespace BaseCore.Products.Abstractions.Services
{
    public interface IBalanceService
    {
        Task AddToBalance(long productId, int amount);
        Task RemoveFromBalance(long productId, int amount);
        Task SetBalance(long productId, int amount);
        Task<int> GetBalance(long productId);
        IQueryable<BalanceDto> GetAllAsNoTracking();
        IQueryable<BalancedProductModel> GetWithBalance(long? cat);
    }
}