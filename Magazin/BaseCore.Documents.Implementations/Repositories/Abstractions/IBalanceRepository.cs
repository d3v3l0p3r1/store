using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Documents.Implementations.Repositories.Abstractions
{
    public interface IBalanceRepository : IRepository<Balance>
    {
        Task<Balance> GetOrCreateBalance(Product product);
    }
}
