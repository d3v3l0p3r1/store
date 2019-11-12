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
    public class OutcomingDocumentService : BaseDocumentService<OutComingDocument, OutComingDocumentEntry>, IOutcomingDocumentService
    {
        public OutcomingDocumentService(IRepository<OutComingDocument> repository, IBalanceService balanceService, IRepository<OutComingDocumentEntry> entryRepository) : base(repository, balanceService, entryRepository)
        {
        }


    }
}
