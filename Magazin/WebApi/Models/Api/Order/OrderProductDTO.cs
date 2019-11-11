using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUiAdmin.Models.Api.Order
{
    public class OrderProductDTO
    {
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal SelledPrice { get; set; }
    }
}
