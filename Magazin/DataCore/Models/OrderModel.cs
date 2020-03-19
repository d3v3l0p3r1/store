using System.Collections.Generic;
using DataCore.Entities;

namespace DataCore.Models
{
    public class OrderModel
    {
        public string Address { get; set; }
        //public string Change { get; set; }
        public string Comment { get; set; }
        //public DeliveryType DeliveryType { get; set; }
        //public DeliveryTime DeliveryTime { get; set; }
        public string Name { get; set; }
        //public int PersonCount { get; set; }
        public string Phone { get; set; }
        public List<OrderModelItem> Products { get; set; }
    }

    public class OrderModelItem
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
