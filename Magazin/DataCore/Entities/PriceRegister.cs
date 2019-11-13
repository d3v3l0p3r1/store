using BaseCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities
{
    public class PriceRegister : BaseEntity
    {
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Date { get; set; }
    }
}
