using DataCore.Entities;
using DataCore.Entities.Documents;
using DataCore.Repositories.Abstract;
using DataCore.Services.Abstract.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete.Documents
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _repository;

        public BalanceService(IBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task AddToBalance(IncomingDocumentEntry incomingDocumentEntry)
        {
            var balance = await _repository.GetOrCreateBalance(incomingDocumentEntry.Product);

            balance.BalanceEntries.Add(new BalanceEntry()
            {
                Count = incomingDocumentEntry.Count,
                IncomingDocument = incomingDocumentEntry.Document
            });

            await _repository.UpdateAsync(balance);
        }

        public async Task RemoveFrombalance(OutComingDocumentEntry entry)
        {
            var balance = await _repository.GetDbSet()
                .Include(x=>x.BalanceEntries)
                .FirstOrDefaultAsync(x => x.ProductId == entry.ProductId && x.ZeroDate == null);
            if (balance == null)
            {
                throw new Exception("Нет товаров для списания");
            }

            var count = await GetBalance(entry.Product);

            if (entry.Count > count)
            {
                throw new Exception("Нет достаточного количества товара для продажи");
            }

            balance.BalanceEntries.Add(new BalanceEntry 
            { 
                Count = -entry.Count, 
                OutcomingDocumentId = entry.DocumentId
            });

            await _repository.UpdateAsync(balance);
        }

        public async Task<int> GetBalance(Product product)
        {
            var query = _repository.GetAllAsNotracking()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == product.Id && x.ZeroDate == null);

            var result = await query.SelectMany(x => x.BalanceEntries).SumAsync(x => x.Count);

            return result;
        }


    }
}
