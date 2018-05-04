using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Order> CreateAsync(int? userID, List<OrderProduct> orderProducts)
        {
            var order = new Order
            {
                Date = DateTime.Now,
                Products = new List<OrderProduct>()
            };


            foreach (var orderProduct in orderProducts)
            {
                order.Products.Add(new OrderProduct
                {
                    ProductID = orderProduct.ProductID,
                    Count = orderProduct.Count
                });
            }

            return Update(order);
        }
    }
}
