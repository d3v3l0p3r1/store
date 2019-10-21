using DataCore.DAL;
using DataCore.Entities;
using DataCore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Repositories.Concrete
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
            var balance = await GetDbSet().FirstOrDefaultAsync(x => x.ProductId == product.Id && x.ZeroDate == null);
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
