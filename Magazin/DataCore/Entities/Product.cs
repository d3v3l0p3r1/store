using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int? FileID { get; set; }
        public virtual FileData File { get; set; }
    }
}
