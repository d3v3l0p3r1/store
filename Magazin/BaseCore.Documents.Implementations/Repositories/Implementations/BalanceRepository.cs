using System.Threading.Tasks;
using BaseCore.DAL.Implementations;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Documents.Implementations.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Documents.Implementations.Repositories.Implementations
{
    public class BalanceRepository : Repository<Balance>, IBalanceRepository
    {
        public BalanceRepository(DataContext context) : base(context)
        {

        }

        public override Task<Balance> CreateAsync(Balance entity)
        {
            return base.CreateAsync(entity);
        }

        public async Task<Balance> GetOrCreateBalance(Product product)
        {
            var balance = await GetAll().FirstOrDefaultAsync(x => x.ProductId == product.Id && x.ZeroDate == null);
            if (balance == null)
            {
                balance = new Balance()
                {
                    ProductId = product.Id
                };

                await CreateAsync(balance);
            }

            return balance;
        }
    }
}
