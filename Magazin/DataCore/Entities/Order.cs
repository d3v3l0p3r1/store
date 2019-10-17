using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BaseCore.Entities;
using BaseCore.Security.Entities;

namespace DataCore.Entities
{
    public class Order : BaseEntity
    {
        public int? UserID { get; set; }
        public virtual User User { get; set; }

        [DisplayName("Дата создания")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }

        [DisplayName("Тип доставки")]
        [EnumDataType(typeof(DeliveryType))]
        public DeliveryType DeliveryType { get; set; }

        [DisplayName("Время доставки")]
        public DeliveryTime DeliveryTime { get; set; }

        [DisplayName("Адрес доставки")]
        public string Address { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [DisplayName("Коментарий")]
        public string Comment { get; set; }

        [DisplayName("Сдача с ...")]
        public string Change { get; set; }

        [DisplayName("Количество приборов")]
        public int PersonCount { get; set; }

        [DisplayName("Имя получателя")]
        public string UserName { get; set; }

        [DisplayName("Статус заказа")]
        public OrderState State { get; set; }

        [DisplayName("Конечная сумма")]
        public decimal TotalAmount { get; set; }
    }

    public class OrderProduct : BaseEntity
    {
        public long ProductID { get; set; }
        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
    }

    public enum DeliveryType
    {
        [Display(Name = "Доставка")]
        Delivery = 0,
        [Display(Name = "Самовывоз")]
        Pickup = 1,
    }


    public enum DeliveryTime
    {
        [Display(Name = "Сегодня")]
        Today = 0,
        [Display(Name = "Завтра")]
        Tomorow = 1,
    }

    public enum OrderState
    {
        [Display(Name = "Новый")]
        New = 0,
        [Display(Name = "В процессе")]
        InProgress = 1,
        [Display(Name = "Завершен")]
        Complete  = 2
    }
}
