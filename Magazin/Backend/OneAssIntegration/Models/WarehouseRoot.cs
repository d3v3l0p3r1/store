using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    public class WarehouseRoot
    {

        [XmlArray("Предложения")]
        [XmlArrayItem("Предложение", typeof(WarehouseItems))]
        public List<WarehouseItems> Items { get; set; }
    }
}
