using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities;

namespace DataCore.Services.Abstract
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<Order> CreateAsync(int? userID, List<OrderProduct> orderProducts);        
    }
}
