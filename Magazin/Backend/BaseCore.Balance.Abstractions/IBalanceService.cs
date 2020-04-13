using System.Linq;
using System.Threading.Tasks;
using BaseCore.Balance.Abstractions.DTO;

namespace BaseCore.Balance.Abstractions
{
    public interface IBalanceService
    {
        Task AddToBalance(long productId, int amount);
        Task RemoveFromBalance(long productId, int amount);
        Task SetBalance(long productId, int amount);
        Task<int> GetBalance(long productId);
        IQueryable<BalanceDto> GetAllAsNoTracking();
    }
}