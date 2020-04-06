using System;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    [Serializable]
    [XmlRoot(ElementName = "КоммерческаяИнформация", Namespace = "urn:1C.ru:commerceml_210")]
    public class Root
    {
        [XmlElement("Классификатор")]
        public Classifier Classifier { get; set; }

        [XmlElement("Каталог")]
        public Catalogue Cataglogue { get; set; }

        [XmlElement("ПакетПредложений")]
        public WarehouseRoot WarehouseRoot { get; set; }
    }
}