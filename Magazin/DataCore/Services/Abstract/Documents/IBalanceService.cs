using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Entities.Documents;

namespace DataCore.Services.Abstract.Documents
{
    public interface IBalanceService
    {
        Task AddToBalance(IncomingDocumentEntry incomingDocumentEntry);
        Task RemoveFrombalance(OutComingDocumentEntry entity);
        Task<int> GetBalance(Product product);
    }
}