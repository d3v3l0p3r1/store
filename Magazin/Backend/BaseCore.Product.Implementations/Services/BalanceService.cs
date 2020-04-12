using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Exceptions.Balance;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Products.Implementations.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IRepository _repository;

        public BalanceService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task AddToBalance(long productId, int amount)
        {
            var balance = await _repository.GetAll<DAL.Implementations.Entities.Balance>()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == productId && x.ZeroDate == null)
                .FirstOrDefaultAsync();

            var balanceEntry = new BalanceEntry { Count = amount };

            if (balance == null)
            {
                balance = new DAL.Implementations.Entities.Balance
                {
                    ProductId = productId,
                    BalanceEntries = new List<BalanceEntry> { balanceEntry }
                };

                await _repository.CreateAsync(balance);
            }
            else
            {
                balance.BalanceEntries.Add(balanceEntry);

                await _repository.UpdateAsync(balance);
            }
        }

        public async Task RemoveFromBalance(long productId, int amount)
        {
            var balanceEntity = await _repository.GetAll<DAL.Implementations.Entities.Balance>()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == productId && x.ZeroDate == null)
                .FirstOrDefaultAsync();

            var balance = await GetBalance(productId);

            if (amount > balance)
            {
                throw new BalanceBelowZeroException();
            }

            if (amount == balance)
            {
                balanceEntity.ZeroDate = DateTime.UtcNow;
            }

            balanceEntity.BalanceEntries.Add(new BalanceEntry()
            {
                Count = -amount,
            });

            await _repository.UpdateAsync(balanceEntity);
        }

        public async Task SetBalance(long productId, int amount)
        {
            var balance = await GetBalance(productId);
            if (balance != 0)
            {
                await SetZeroBalance(productId);
            }

            await AddToBalance(productId, amount);
        }

        private async Task SetZeroBalance(long productId)
        {
            var balanceEntity = await _repository.GetAll<DAL.Implementations.Entities.Balance>()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == productId && x.ZeroDate == null)
                .FirstOrDefaultAsync();

            var balance = await GetBalance(productId);
            balanceEntity.ZeroDate = DateTime.UtcNow;
            balanceEntity.BalanceEntries.Add(new BalanceEntry { Count = -balance });
            await _repository.UpdateAsync(balanceEntity);
        }

        public async Task<int> GetBalance(long productId)
        {
            var query = _repository.GetAllAsNotracking<DAL.Implementations.Entities.Balance>()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == productId && x.ZeroDate == null);

            var result = await query.SelectMany(x => x.BalanceEntries).SumAsync(x => x.Count);

            return result;
        }

        public IQueryable<BalanceDto> GetAllAsNoTracking()
        {
            return _repository.GetAll<Balance>()
                .Include(x => x.BalanceEntries)
                .AsNoTracking()
                .Select(x => new BalanceDto
                {
                    ProductId = x.ProductId,
                    ZeroDate = x.ZeroDate,
                    BalanceEntries = x.BalanceEntries.Select(z => new BalanceEntryDto()
                    {
                        Count = z.Count
                    }),
                });
        }

        private IQueryable<BalanceProduct> GetBalanceQuery()
        {
            var productsQuery = _repository.GetAllAsNotracking<Product>();
            var balanceQuery = _repository.GetAllAsNotracking<Balance>();
            var t = from product in productsQuery
                    join balance in balanceQuery on product.Id equals balance.ProductId
                    where balance.ZeroDate == null && balance.BalanceEntries.Sum(z => z.Count) > 0
                    select new BalanceProduct()
                    {
                        Balance = balance,
                        Product = product
                    };

            return t;
        }

        public IQueryable<BalancedProductModel> GetWithBalance(long? cat)
        {
            var query = GetBalanceQuery();

            if (cat != null)
            {
                query = query.Where(x => x.Product.CategoryId == cat.Value);
            }

            var t = query.Select(x => new BalancedProductModel
            {
                Id = x.Product.Id,
                CateogryId = x.Product.Category.Id,
                Title = x.Product.Title,
                Description = x.Product.Description,
                FileId = x.Product.FileId,
                KindId = x.Product.KindId,
                KingTitle = x.Product.Kind.Title,
                Price = x.Product.Price,
                Count = x.Balance.BalanceEntries.Sum(z => z.Count),
            });

            return t;
        }

        public async Task<List<ProductDto>> GetLastProducts(int count)
        {
            var query = GetBalanceQuery();
            var products = await query.OrderByDescending(x => x.Product.UpdateTime)
                .Select(x => new ProductDto
                {
                    Id = x.Product.Id,
                    Title = x.Product.Title,
                    Description =  x.Product.Description,
                    Price = x.Product.Price,
                    Kind = x.Product.Kind.Title,
                    Amount = x.Balance.BalanceEntries.Sum(z=>z.Count),
                    FileId = x.Product.FileId
                })
                .Take(count)
                .ToListAsync();

            return products;
        }

        private class BalanceProduct
        {
            public Balance Balance { get; set; }
            public Product Product { get; set; }
        }
    }
}
