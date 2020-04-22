using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Products.Abstractions.Models
{
    public class BrandDetailDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? FileId { get; set; }
        public string Description { get; set; }
        public FileData File { get; set; }
    }
}
