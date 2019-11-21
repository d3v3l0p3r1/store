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
        Task AddToBalance<T>(T entity) where T : BaseDocumentEntry;
        Task RemoveFrombalance<T>(T entity) where T : BaseDocumentEntry;
        Task<int> GetBalance(Product product);
        Task<Balance> Get(Product product);
        IQueryable<Balance> GetDbSet();
        IQueryable<Balance> GetAllAsNoTracking();
        IQueryable<BalancedProductModel> GetProductBalance(long? categoryId, string schema, string host);
        IQueryable<Balance> GetAll();
    }
}