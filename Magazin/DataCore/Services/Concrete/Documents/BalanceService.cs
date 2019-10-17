﻿using DataCore.Entities;
using DataCore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete.Documents
{
    public class BalanceService
    {
        private readonly IBalanceRepository _repository;

        public BalanceService(IBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task AddToBalance(Product product, int count)
        {
            var balance = new Balance()
            {
                Count = count,
                Product = product
            };

            if (count < 0)
            {
                var balanceCount = await GetBalance(product);
                var absoluteCount = Math.Abs(count);
                if (balanceCount < absoluteCount)
                {
                    throw new Exception("Нет необходимого количества товара");
                }

                if (balanceCount == absoluteCount)
                {
                    balance.ZeroDate = DateTime.Now;
                }
            }

            await _repository.CreateAsync(balance);
        }

        public async Task<int> GetBalance(Product product)
        {
            var query = _repository.GetAll().Where(x => x.ProductId == product.Id && x.ZeroDate == null);

            var result = await query.SumAsync(x => x.Count);

            return result;
        }



    }
}
