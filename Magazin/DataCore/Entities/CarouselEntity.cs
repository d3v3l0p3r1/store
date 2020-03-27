using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class Carousel : BaseEntity
    {
        public long FileId { get; set; }
        public FileData File { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public string Href { get; set; }

        public bool Show { get; set; }

    }
}
