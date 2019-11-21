using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Admin.ProductPrice
{
    /// <summary>
    /// Модель цены, цены сгруппированные по продукту
    /// </summary>
    public class ProductPriceModel
    {
        /// <summary>
        /// Id product
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Цены
        /// </summary>
        public IEnumerable<ProductPriceModelItem> Prices { get; set; }
    }

    /// <summary>
    /// Цены
    /// </summary>
    public class ProductPriceModelItem 
    {
        /// <summary>
        /// Product price id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата цены
        /// </summary>
        public DateTime Date { get; set; }
    }
}
