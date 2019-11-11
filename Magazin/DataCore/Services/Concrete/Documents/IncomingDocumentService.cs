using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete.Documents
{
    public class IncomingDocumentService : BaseService<IncomingDocument>, IIncomingDocumentService
    {
        private readonly IBalanceService _balanceService;
        private readonly IRepository<IncomingDocumentEntry> _incomingDocumentRepository;

        public IncomingDocumentService(IRepository<IncomingDocument> repository, IBalanceService balanceService, IRepository<IncomingDocumentEntry> incomingDocumentRepository) : base(repository)
        {
            _balanceService = balanceService;
            _incomingDocumentRepository = incomingDocumentRepository;
        }

        public override Task<IncomingDocument> GetAsync(long id)
        {
            return _repository.GetDbSet()
                .Include(x => x.Entries)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IncomingDocument> UpdateAsync(IncomingDocument entity)
        {
            if (!entity.Entries.Any())
            {
                throw new Exception("Должна быть хотя бы одна позиция");
            }

            var allEntries = await _incomingDocumentRepository.GetAllAsNotracking().Where(x => x.DocumentId == entity.Id).ToListAsync();
            var entryIds = entity.Entries.Select(x => x.Id);
            var toDelete = allEntries.Where(x => !entryIds.Contains(x.Id));
            await _incomingDocumentRepository.DeleteAsync(toDelete);

            return await base.UpdateAsync(entity);
        }

        public override async Task DeleteAsync(long id)
        {
            var original = await GetAsync(id);
            if (original.DocumentStatus == DocumentStatus.Processed)
            {
                throw new Exception("Документ проведен, удаление невозможно");
            }

            await base.DeleteAsync(id);
        }

        public override async Task<IncomingDocument> CreateAsync(IncomingDocument entity)
        {
            if (!entity.Entries.Any())
            {
                throw new Exception("Должна быть хотя бы одна позиция");
            }

            entity.DocumentStatus = DocumentStatus.New;
            entity.Date = DateTime.Now;

            if (String.IsNullOrEmpty(entity.Title))
            {
                entity.Title = "Документ прихода от " + entity.Date.ToString("d");
            }

            await base.CreateAsync(entity);

            return entity;
        }

        public async Task Apply(long id)
        {
            var original = await GetAsync(id);

            if (original.DocumentStatus == DocumentStatus.Processed)
            {
                throw new Exception("Документ уже проведен");
            }

            foreach (var entry in original.Entries)
            {
                await _balanceService.AddToBalance(entry);
            }

            original.DocumentStatus = DocumentStatus.Processed;
            original.ProcessDate = DateTime.Now;

            await UpdateAsync(original);
        }

        public async Task Discard(long id)
        {
            var original = await GetAsync(id);
            
            if (original.DocumentStatus == DocumentStatus.Processed)
            {
                foreach (var entry in original.Entries)
                {
                    entry.Count = -entry.Count;
                    await _balanceService.AddToBalance(entry);
                }
            }

            original.DocumentStatus = DocumentStatus.Canceled;
            original.ProcessDate = null;

            await UpdateAsync(original);
        }
    }
}
