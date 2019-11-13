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
    public class IncomingDocumentService : BaseDocumentService<IncomingDocument, IncomingDocumentEntry>, IIncomingDocumentService
    {
        private readonly IBalanceService _balanceService;
        public IncomingDocumentService(IRepository<IncomingDocument> repository, IBalanceService balanceService, IRepository<IncomingDocumentEntry> incomingDocumentRepository) : base(repository, balanceService, incomingDocumentRepository)
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
