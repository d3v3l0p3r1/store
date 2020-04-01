using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Implementations.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Documents.Implementations.Services
{
    public class IncomingDocumentService : BaseDocumentService<IncomingDocument, IncomingDocumentEntry>, IIncomingDocumentService
    {
        private readonly IBalanceService _balanceService;
        private readonly IRepository<IncomingDocument> _repository;
        public IncomingDocumentService(IRepository<IncomingDocument> repository, IBalanceService balanceService, IRepository<IncomingDocumentEntry> incomingDocumentRepository) : base(repository, balanceService, incomingDocumentRepository)
        {
            _repository = repository;
            _balanceService = balanceService;
        }

        public Task<IncomingDocument> GetAsync(long id)
        {
            return _repository.GetAll()
               .Where(x => x.Id == id)
               .Include(x => x.Author)
               .Include(x => x.Shipper)
               .Include(x => x.Entries)
               .ThenInclude(x => x.Product)
               .FirstOrDefaultAsync();
        }

        public Task<IncomingDocument> CreateAsync(IncomingDocument entity)
        {
            entity.Total = entity.Entries.Sum(x => x.Price * x.Count);

            return _repository.CreateAsync(entity);
        }

        public Task<IncomingDocument> UpdateAsync(IncomingDocument entity)
        {
            entity.Total = entity.Entries.Sum(x => x.Price * x.Count);

            return base.UpdateAsync(entity);
        }

        public override async Task Apply(long id)
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

        public override async Task Discard(long id)
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
