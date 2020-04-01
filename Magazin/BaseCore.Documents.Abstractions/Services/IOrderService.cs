using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Models;

namespace BaseCore.Documents.Implementations.Services.Abstractions
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(long? userID, OrderModel model);
        Task<List<OrderProduct>> GetOrderProducts(long orderID);
        IQueryable<Order> GetAllAsNotracking();
        Task<Order> GetAsync(long id);
        Task UpdateAsync(Order order);
    }
}
