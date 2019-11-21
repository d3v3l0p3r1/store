using BaseCore.Services.Abstract;
using DataCore.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Abstract.Documents
{
    public interface IBaseDocumentService<T, TEntry> : IBaseService<T> 
        where T: BaseDocument<TEntry>
        where TEntry : BaseDocumentEntry
    {
        Task Apply(long id);
        Task Discard(long id);
    }
}
