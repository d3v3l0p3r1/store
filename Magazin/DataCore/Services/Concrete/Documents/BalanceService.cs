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

        public async Task<int> GetBalance(Product product)
        {
            var query = _repository.GetAll()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == product.Id && x.ZeroDate == null);

            var result = await query.SelectMany(x => x.BalanceEntries).SumAsync(x => x.Count);

            return result;
        }



    }
}
