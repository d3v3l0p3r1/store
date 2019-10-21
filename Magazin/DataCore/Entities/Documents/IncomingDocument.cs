using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public class IncomingDocument : BaseDocument
    {
        public List<IncomingDocumentEntry> Entries { get; set; } = new List<IncomingDocumentEntry>();
    }



    public class IncomingDocumentEntry: BaseDocumentEntry<IncomingDocument>
    {
        
    }
}
