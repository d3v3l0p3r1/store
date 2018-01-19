﻿using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }

    }

    public class OrderProduct : BaseEntity
    {
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public decimal Price { get; set; }
    }
}