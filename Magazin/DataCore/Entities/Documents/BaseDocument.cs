using BaseCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public abstract class BaseDocument : BaseEntity
    {
        public DateTime Date { get; set; }
    }
}
