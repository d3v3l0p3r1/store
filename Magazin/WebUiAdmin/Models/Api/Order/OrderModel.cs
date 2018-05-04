using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;

namespace WebUiAdmin.Models.Api.Order
{
    public class OrderModel
    {        
        public List<OrderModelItem> Products { get; set; }
    }

    public class OrderModelItem
    {
        public int ProductID { get; set; }
        public int Count { get; set; }
    }
}
