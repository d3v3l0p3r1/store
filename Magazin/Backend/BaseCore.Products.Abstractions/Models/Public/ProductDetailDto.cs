using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Products.Abstractions.Models.Public
{
    public class ProductDetailDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long FileId { get; set; }
        public List<long> Files { get; set; }
        public string VenderCode { get; set; }
        public string MeasureUnit { get; set; }
        public string Package { get; set; }
    }
}
