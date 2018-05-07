﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Repositories.Concrete
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public override Order Find(int id)
        {
            var dbSet = _dataContext.Set<Order>();

            var order = dbSet.Where(x => x.Id == id).Include(x => x.User).Include(x => x.Products).ThenInclude(x => x.Product).FirstOrDefault();

            return order;
        }

        public IQueryable<OrderProduct> GetOrderProducts(int orderID)
        {
            return _dataContext.OrderProducts.Where(x => x.OrderID == orderID).Include(x => x.Product);
        }
    }
}
