using BaseCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public abstract class BaseDocumentEntry: BaseEntity
    {
        public long DocumentId { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
