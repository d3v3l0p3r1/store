using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public class IncomingDocument : BaseDocument
    {
        public List<IncomingDocumentEntry> Entries { get; set; }
    }



    public class IncomingDocumentEntry: BaseDocumentEntry<IncomingDocument>
    {
        
    }
}
