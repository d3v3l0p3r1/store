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
        public IncomingDocumentService(IRepository<IncomingDocument> repository, IBalanceService balanceService, IRepository<IncomingDocumentEntry> incomingDocumentRepository) : base(repository, balanceService, incomingDocumentRepository)
        {
        }

       
    }
}
