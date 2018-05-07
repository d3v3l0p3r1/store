using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCore.Services.Abstract;
using DataCore.Entities;

namespace DataCore.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<OrderProduct> GetOrderProducts(int orderID);
    }
}
