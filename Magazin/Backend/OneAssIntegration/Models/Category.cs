using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    public class Category
    {
        [XmlElement("Ид")]
        public string Id { get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }
    }
}
