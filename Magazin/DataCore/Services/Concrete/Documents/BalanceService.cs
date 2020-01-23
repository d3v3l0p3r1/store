﻿using DataCore.Entities;
using DataCore.Entities.Documents;
using DataCore.Exceptions.Balance;
using DataCore.Models;
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

        public async Task AddToBalance<T>(T incomingDocumentEntry) where T : BaseDocumentEntry
        {
            var balance = await _repository.GetOrCreateBalance(incomingDocumentEntry.Product);

            var balanceEntry = new BalanceEntry()
            {
                Count = incomingDocumentEntry.Count,
                IncomingDocumentId = incomingDocumentEntry.DocumentId
            };

            balance.BalanceEntries.Add(balanceEntry);

            await _repository.UpdateAsync(balance);
        }

        public async Task RemoveFrombalance<T>(T entry) where T : BaseDocumentEntry
        {
            var balance = await _repository.GetAll()
                .Include(x => x.BalanceEntries)
                .FirstOrDefaultAsync(x => x.ProductId == entry.ProductId && x.ZeroDate == null);
            if (balance == null)
            {
                throw new Exception("Нет товаров для списания");
            }

            var count = await GetBalance(entry.Product);

            if (entry.Count > count)
            {
                throw new BalanceBelowZeroException();
            }

            if (entry.Count == count)
            {
                balance.ZeroDate = DateTime.Now;
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

        public async Task<Balance> Get(Product product)
        {
            var query = _repository.GetAllAsNotracking()
                .Include(x => x.BalanceEntries)
                .Where(x => x.ProductId == product.Id && x.ZeroDate == null);

            return await query.SingleOrDefaultAsync();
        }

        public IQueryable<Balance> GetDbSet()
        {
            return _repository.GetAll();
        }

        public IQueryable<Balance> GetAllAsNoTracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public IQueryable<BalancedProductModel> GetProductBalance(long? categoryId, string schema, string host)
        {
            var url = $"{schema}://{host}/File/GetFile/";

            var query = _repository.GetAllAsNotracking()
                .Include(x => x.Product)
                .Include(x => x.Product.Kind)
                .Include(x => x.BalanceEntries)
                .Where(x => x.ZeroDate == null && x.BalanceEntries.Sum(z => z.Count) > 0);

            if (categoryId != null)
            {
                query = query.Where(x => x.Product.CategoryId == categoryId.Value);
            }

            var result = query.Select(x => new BalancedProductModel
            {
                Count = x.BalanceEntries.Sum(z => z.Count),
                CateogryId = x.Product.CategoryId,
                Description = x.Product.Description,
                File = x.Product.FileId != null ? url + x.Product.FileId : url + 1,
                KindId = x.Product.KindId,
                Id = x.ProductId,
                KingTitle = x.Product.Kind.Title,
                Price = 0,
                Title = x.Product.Title
            });

            return result;
        }

        public IQueryable<Balance> GetAll()
        {
            var query = _repository.GetAllAsNotracking()
                .Include(x => x.Product)
                .Include(x => x.Product.Kind)
                .Include(x => x.BalanceEntries)
                    .ThenInclude(x => x.IncomingDocument)
                    .ThenInclude(x => x.Entries)
                .Include(x => x.BalanceEntries)
                    .ThenInclude(x => x.OutComingDocument)
                    .ThenInclude(x => x.Entries);

            return query;
        }
    }
}
