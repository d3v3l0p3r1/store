using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Entities.Documents;
using DataCore.Models;

namespace DataCore.Services.Abstract.Documents
{
    public interface IBalanceService
    {
        Task AddToBalance<T>(BaseDocumentEntry<T> incomingDocumentEntry) where T : BaseDocument;
        Task RemoveFrombalance<T>(BaseDocumentEntry<T> entity) where T : BaseDocument;
        Task<int> GetBalance(Product product);
        Task<Balance> Get(Product product);
        IQueryable<Balance> GetDbSet();
        IQueryable<Balance> GetAllAsNoTracking();
        Task<List<BalancedProductModel>> GetProductBalance(long categoryId, string schema, string host);
    }
}