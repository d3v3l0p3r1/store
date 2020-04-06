using System;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Abstractions.Services;
using BaseCore.Products.Abstractions.Services;

namespace BaseCore.Documents.Implementations.Services
{
    public class OutcomingDocumentService : BaseDocumentService<OutComingDocument, OutComingDocumentEntry>, IOutcomingDocumentService
    {
        private readonly IBalanceService _balanceService;
        public OutcomingDocumentService(IRepository<OutComingDocument> repository, IBalanceService balanceService, IRepository<OutComingDocumentEntry> entryRepository) : base(repository, balanceService, entryRepository)
        {
            _balanceService = balanceService;
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
                await _balanceService.RemoveFromBalance(entry.ProductId, entry.Count);
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
                    entry.Count = entry.Count;
                    await _balanceService.AddToBalance(entry.ProductId, entry.Count);
                }
            }

            original.DocumentStatus = DocumentStatus.Canceled;
            original.ProcessDate = null;

            await UpdateAsync(original);
        }
    }
}
