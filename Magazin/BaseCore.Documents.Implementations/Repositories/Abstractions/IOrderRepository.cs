using System.Linq;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Documents.Implementations.Repositories.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<OrderProduct> GetOrderProducts(long orderID);
    }
}
