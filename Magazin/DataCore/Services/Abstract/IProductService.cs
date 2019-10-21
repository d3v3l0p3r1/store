using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities;
using DataCore.Models;

namespace DataCore.Services.Abstract
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<BalancedProductModel>> GetWithBalance(long cat, string schema, string host);
    }
}
