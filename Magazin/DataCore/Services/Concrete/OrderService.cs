﻿using System;
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

        public async Task<Order> CreateAsync(int? userID, OrderModel model)
        {
            var order = new Order
            {
                UserID = userID,
                Date = DateTime.Now,
                DeliveryTime = model.DeliveryTime,
                DeliveryType = model.DeliveryType,
                Address = model.Address,
                Phone = model.Phone,
                Comment = model.Comment,
                Change = model.Change,
                PersonCount = model.PersonCount,
                UserName = model.Name,

                Products = new List<OrderProduct>()
            };

            var productIds = model.Products.Select(x => x.Product.Id);

            var products = _productService.GetAll()
                    .Where(x => productIds.Contains(x.Id))
                    .Distinct()
                    .ToList()
                    .Join(model.Products, x => x.Id, x => x.Product.Id, (p, m) =>
                    new
                    {
                        p.Id,
                        p.Price,
                        m.Count,

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


            order = await CreateAsync(order);

            return order;
        }

        public override Order Update(Order entity)
        {
            RecalculateTotalAmount(entity);

            return base.Update(entity);
        }

        public IQueryable<OrderProduct> GetOrderProducts(int orderID)
        {
            return _rep.GetOrderProducts(orderID);
        }

        private void RecalculateTotalAmount(Order order)
        {
            var total = order.Products.Sum(x => x.Price * x.Count);
            order.TotalAmount = total;

        }
    }
}
