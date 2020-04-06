using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Documents.Implementations.Repositories.Abstractions;
using BaseCore.Documents.Implementations.Services.Abstractions;
using BaseCore.Products.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Documents.Implementations.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _rep;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository repository, IProductService productService)
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
                State = OrderState.New,
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
                        p.Price
                    });



            foreach (var product in products)
            {
                var orderProduct = new OrderProduct
                {
                    ProductID = product.Id,
                    Count = product.Count,
                    Price = product.Price
                };

                order.Products.Add(orderProduct);
            }

            RecalculateTotalAmount(order);

            order = await _rep.CreateAsync(order);

            return order;
        }

        public Task<Order> GetAsync(long id)
        {
            return _rep.GetAsync(id);
        }

        Task IOrderService.UpdateAsync(Order order)
        {
            return UpdateAsync(order);
        }

        public Task<Order> UpdateAsync(Order entity)
        {
            RecalculateTotalAmount(entity);

            return _rep.UpdateAsync(entity);
        }

        public async Task<List<OrderProduct>> GetOrderProducts(long orderID)
        {
            var query = _rep.GetOrderProducts(orderID);
            var result = await query.ToListAsync();

            return result;
        }

        public IQueryable<Order> GetAllAsNotracking()
        {
            return _rep.GetAllAsNotracking();
        }

        private void RecalculateTotalAmount(Order order)
        {
            var total = order.Products.Sum(x => x.Price * x.Count);
            order.TotalAmount = total;
        }


        protected void PrepareEntity(Order entity)
        {
            var newProducts = entity.Products.Where(x => x.Id == 0);

            foreach (var orderProduct in newProducts)
            {
                orderProduct.Product.Category = null;
            }
        }
    }
}
