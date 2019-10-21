using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete.Documents
{
    public class IncomingDocumentService : BaseService<IncomingDocument>, IIncomingDocumentService
    {
        private readonly IBalanceService _balanceService;

        public IncomingDocumentService(IRepository<IncomingDocument> repository, IBalanceService balanceService) : base(repository)
        {
            _balanceService = balanceService;
        }

        public override Task<IncomingDocument> GetAsync(long id)
        {
            return _repository.GetDbSet()
                .Include(x => x.Entries)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IncomingDocument> CreateAsync(IncomingDocument entity)
        {
            entity.DocumentStatus = DocumentStatus.New;
            entity.Date = DateTime.Now;

            await base.CreateAsync(entity);

            return entity;
        }

        public async Task Apply(IncomingDocument document)
        {
            var original = await GetAsync(document.Id);

            if (original.DocumentStatus == DocumentStatus.Processed)
            {
                throw new Exception("Документ уже проведен");
            }

            original.Entries = document.Entries;

            foreach (var entry in document.Entries)
            {
                await _balanceService.AddToBalance(entry);
            }

            original.DocumentStatus = DocumentStatus.Processed;
            original.ProcessDate = DateTime.Now;

            await UpdateAsync(original);
        }
    }
}
