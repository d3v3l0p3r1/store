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
    public class OutcomingDocumentService : BaseService<OutComingDocument>
    {
        private readonly IBalanceService _balanceService;

        public OutcomingDocumentService(IRepository<OutComingDocument> repository, IBalanceService balanceService) : base(repository)
        {
            _balanceService = balanceService;
        }

        public override async Task<OutComingDocument> CreateAsync(OutComingDocument entity)
        {
            await base.CreateAsync(entity);

            foreach(var entry in entity.Entry)
            {
                await _balanceService.RemoveFrombalance(entry);
            }


            return entity;
        }


    }
}
