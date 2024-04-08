using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    /// <summary>
    /// Элемент справочник, определяет ти справочника
    /// </summary>
    public class Property
    {
        [XmlElement(ElementName = "Ид")]
        public string Id { get; set; }

        [XmlElement(ElementName = "Наименование")]
        public string Name { get; set; }

        [XmlArray("ВариантыЗначений")]
        [XmlArrayItem("Справочник", typeof(Properties))]
        public List<Properties> Properties { get; set; }
    }

    /// <summary>
    /// Значение справочника
    /// </summary>
    public class Properties
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [XmlElement("ИдЗначения")]
        public string Id { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [XmlElement("Значение")]
        public string Value { get; set; }
    }
}
