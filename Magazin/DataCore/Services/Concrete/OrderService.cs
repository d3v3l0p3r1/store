using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;

namespace DataCore.Services.Concrete
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IRepository<Order> repository) : base(repository)
        {
        }
    }
}
