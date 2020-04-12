using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Products.Abstractions.Models
{
    public class BrandDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? FileId { get; set; }
    }
}
