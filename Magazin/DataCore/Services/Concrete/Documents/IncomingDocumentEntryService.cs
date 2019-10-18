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
    public class IncomingDocumentService : BaseService<IncomingDocument>
    {
        private readonly IBalanceService _balanceService;

        public IncomingDocumentService(IRepository<IncomingDocument> repository, IBalanceService balanceService) :base(repository)
        {
            _balanceService = balanceService;
        }

        public override async Task<IncomingDocument> CreateAsync(IncomingDocument entity)
        {
            await base.CreateAsync(entity);

            foreach(var entry in entity.Entries)
            {
                await _balanceService.AddToBalance(entry);
            }

            return entity;
        }
    }
}
