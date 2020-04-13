using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    public class WarehouseItems
    {
        [XmlElement("Ид")]
        public string Id { get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }

        [XmlElement("Количество")]
        public int Amount { get; set; }

        [XmlArray("Цены")]
        [XmlArrayItem("Цена", typeof(Price))]
        public List<Price> Prices { get; set; }

    }

    public class Price
    {
        [XmlElement("ЦенаЗаЕдиницу")]
        public decimal Value { get; set; }

        [XmlElement("Валюта")]
        public string Currency { get; set; }
    }
}
