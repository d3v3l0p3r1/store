using BaseCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public abstract class BaseDocument : BaseEntity
    {
        
        public DateTime Date { get; set; }
        public DateTime? ProcessDate { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
