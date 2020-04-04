using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Documents.Implementations.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Documents.Implementations.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public override async Task<Order> GetAsync(long id)
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
            return _dataContext.Set<OrderProduct>()
                .Include(x => x.Product)
                .Where(x => x.OrderID == orderID);
        }
    }
}
