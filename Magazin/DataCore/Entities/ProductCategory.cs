using System.Collections.Generic;
using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
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

        /// <summary>
        /// Имя маршрута
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// Внешний идентификатор
        /// </summary>
        public string ExternalId { get; set; }

    }
}
