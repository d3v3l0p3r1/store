using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public class OutComingDocument : BaseDocument
    {
        public List<OutComingDocumentEntry> Entry { get; set; } = new List<OutComingDocumentEntry>();
    }

    public class OutComingDocumentEntry: BaseDocumentEntry<OutComingDocument>
    {
        
    }
}
