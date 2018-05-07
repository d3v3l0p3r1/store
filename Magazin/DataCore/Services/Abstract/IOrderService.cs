using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities;
using DataCore.Models;

namespace DataCore.Services.Abstract
{
    public interface IOrderService : IBaseService<Order>
    {        
        Task<Order> CreateAsync(int? userID, OrderModel model);
        IQueryable<OrderProduct> GetOrderProducts(int orderID);
    }
}
