using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override async Task<Order> FindAsync(long id)
        {
            var dbSet = _dataContext.Set<Order>();

            var order = await dbSet.Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.Products)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();

            return order;
        }

        public IQueryable<OrderProduct> GetOrderProducts(long orderID)
        {
            return _dataContext.OrderProducts.Where(x => x.OrderID == orderID).Include(x => x.Product);
        }
    }
}
