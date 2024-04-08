using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Balance.Abstractions;
using BaseCore.Balance.Abstractions.DTO;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Exceptions.Balance;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Balance.Implementations.Services
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
            await SetZeroBalance(productId);
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
            return _repository.GetAll<DAL.Implementations.Entities.Balance>()
                .Include(x => x.BalanceEntries)
                .AsNoTracking()
                .Select(x => new BalanceDto()
                {
                    ProductId = x.ProductId,
                    ZeroDate = x.ZeroDate,
                    BalanceEntries = x.BalanceEntries.Select(z => new BalanceEntryDto()
                    {
                        Count = z.Count
                    }),
                });
        }
    }
}
