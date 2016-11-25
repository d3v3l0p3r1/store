using System;
using System.Collections.Generic;
using Base.Entities;

namespace Data.Entities
{
    public class Order : BaseEntity
    {
        public virtual ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        New = 0,
        InWork = 1,
        Complete = 2
    }

    public class OrderProduct : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
