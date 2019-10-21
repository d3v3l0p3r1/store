using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
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

        public override async Task<IncomingDocument> CreateAsync(IncomingDocument entity)
        {
            entity.DocumentStatus = DocumentStatus.New;
            entity.Date = DateTime.Now;

            await base.CreateAsync(entity);

            return entity;
        }

        public async Task Apply(long id)
        {
            var entity = await FindAsync(id);

            foreach (var entry in entity.Entries)
            {
                await _balanceService.AddToBalance(entry);
            }

            entity.DocumentStatus = DocumentStatus.Processed;
            entity.ProcessDate = DateTime.Now;

            await UpdateAsync(entity);
        }
    }
}
