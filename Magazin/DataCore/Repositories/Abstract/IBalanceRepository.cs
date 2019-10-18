using BaseCore.Services.Abstract;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Repositories.Abstract
{
    public interface IBalanceRepository : IRepository<Balance>
    {
        Task<Balance> GetOrCreateBalance(Product product);
    }
}
