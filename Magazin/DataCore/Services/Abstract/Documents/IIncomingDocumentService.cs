using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities.Documents;

namespace DataCore.Services.Abstract.Documents
{
    public interface IIncomingDocumentService : IBaseDocumentService<IncomingDocument, IncomingDocumentEntry>
    {
    }
}