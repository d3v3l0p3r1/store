using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities.Documents;

namespace DataCore.Services.Abstract.Documents
{
    public interface IIncomingDocumentService : IBaseService<IncomingDocument>
    {
        Task Apply(IncomingDocument document);
    }
}