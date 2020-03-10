using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Models;
using DataCore.Repositories.Abstract;
using DataCore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Services.Concrete
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _rep;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository repository, IProductService productService) : base(repository)
        {
            _rep = repository;
            _productService = productService;
        }

        public async Task<Order> CreateAsync(long? userID, OrderModel model)
        {
            var order = new Order
            {
                UserID = userID,
                Date = DateTime.Now,
                Address = model.Address,
                Phone = model.Phone,
                Comment = model.Comment,
                UserName = model.Name,
                Products = new List<OrderProduct>()
            };

            var productIds = model.Products.Select(x => x.Product.Id);

            var products = _productService.GetAllAsNotracking()
                    .Where(x => productIds.Contains(x.Id))
                    .Distinct()
                    .ToList()
                    .Join(model.Products, x => x.Id, x => x.Product.Id, (p, m) =>
                    new
                    {
                        p.Id,
                        m.Count,

                    });



            foreach (var product in products)
            {
                var orderProduct = new OrderProduct
                {
                    ProductID = product.Id,
                    Count = product.Count,
                };

                order.Products.Add(orderProduct);
            }

            RecalculateTotalAmount(order);


            order = await CreateAsync(order);

            return order;
        }

        public override Task<Order> UpdateAsync(Order entity)
        {
            RecalculateTotalAmount(entity);

            return base.UpdateAsync(entity);
        }

        public async Task<List<OrderProduct>> GetOrderProducts(long orderID)
        {
            var query = _rep.GetOrderProducts(orderID);
            var result = await query.ToListAsync();

            return result;
        }

        private void RecalculateTotalAmount(Order order)
        {
            var total = order.Products.Sum(x => x.Price * x.Count);
            order.TotalAmount = total;
        }


        protected override void PrepareEntity(Order entity)
        {
            var newProducts = entity.Products.Where(x => x.Id == 0);

            foreach (var orderProduct in newProducts)
            {
                orderProduct.Product.Category = null;
            }

            base.PrepareEntity(entity);
        }
    }
}
