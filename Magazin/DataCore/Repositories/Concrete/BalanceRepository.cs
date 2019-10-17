using DataCore.DAL;
using DataCore.Entities;
using DataCore.Repositories.Abstract;
using System;
using System.Collections.Generic;
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
    }
}
