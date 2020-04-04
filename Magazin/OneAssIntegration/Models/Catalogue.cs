using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    public class Catalogue
    {
        [XmlArray("Товары")]
        [XmlArrayItem("Товар", typeof(CatalogueItem))]
        public List<CatalogueItem> Items { get; set; }
    }

    /// <summary>
    /// Единица каталога
    /// </summary>
    public class CatalogueItem
    {
        /// <summary>
        /// Id
        /// </summary>
        [XmlElement("Ид")]
        public string Id { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        [XmlElement("Артикул")]
        public string Article { get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }

        [XmlArray("Группы")]
        [XmlArrayItem("Ид", typeof(string))]
        public List<string> Groups { get; set; }

        [XmlElement("Категория")]
        public string CategoryId { get; set; }

        [XmlElement("Описание")]
        public string Description { get; set; }

        [XmlArray("ЗначенияСвойств")]
        [XmlArrayItem("ЗначенияСвойства", typeof(ItemKeyValue))]
        public List<ItemKeyValue> Props { get; set; }

        /// <summary>
        /// Реквизиты
        /// </summary>
        [XmlArray("ЗначенияРеквизитов")]
        [XmlArrayItem("ЗначениеРеквизита", typeof(ItemNameValue))]
        public List<ItemNameValue> NapeProps { get; set; }

        [XmlElement("БазоваяЕдиница")]
        public MeasureUnit MeasureUnit { get; set; }
    }

    public class ItemKeyValue
    {
        [XmlElement("Ид")]
        public string Key { get; set; }

        [XmlElement("Значение")]
        public string Value { get; set; }
    }

    public class ItemNameValue
    {
        [XmlElement("Наименование")]
        public string Name { get; set; }

        [XmlElement("Значение")]
        public string Value { get; set; }
    }

    public class MeasureUnit
    {
        [XmlAttribute("Код")]
        public string Code { get; set; }

        [XmlAttribute("НаименованиеПолное")]
        public string Name { get; set; }
    }
}
