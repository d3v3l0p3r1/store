using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Services;
using Data.Entities;

namespace Data.Services
{
    public interface IOrderService : IBaseService<Order>
    {
        Order CreateOrder(IUnitOfWork uow, Bascet bascet, string phone, string address);
    }


    public class OrderService : BaseService<Order>, IOrderService
    {
        public Order CreateOrder(IUnitOfWork uow, Bascet bascet, string phone, string address)
        {
            if (bascet == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(phone))
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException();

            if (bascet.Products.Any(x => x.Count <= 0))
                throw new ArgumentException();

            var order = new Order
            {
                Address = address,
                Phone = phone,
                Date = DateTime.Now,
                Status = OrderStatus.New,
                Products = bascet.Products.Select(x => new OrderProduct()
                {
                    Count = x.Count,
                    Price = x.Product.Price,
                    ProductId = x.Product.Id

                }).ToList()
            };

            return this.Update(uow, order);
        }

    }
}
