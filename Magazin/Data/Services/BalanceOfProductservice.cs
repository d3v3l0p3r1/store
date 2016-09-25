using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Services;
using Data.Entities;

namespace Data.Services
{
    public interface IBalanceOfProductService : IBaseService<BalanceOfProduct>
    {
    }

    public class BalanceOfProductService : BaseService<BalanceOfProduct>, IBalanceOfProductService
    {
    }
}
