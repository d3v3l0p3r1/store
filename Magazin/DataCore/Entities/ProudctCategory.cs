using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; set; }
        public decimal SortOrder { get; set; }

        public long? ParentId { get; set; }
        public ProductCategory Parent { get; set; }

        public List<ProductCategory> Childs { get; set; }

        /// <summary>
        /// Маска категории
        /// </summary>
        public string Mask { get; set; }
    }
}
