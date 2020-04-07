using System.Collections.Generic;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    public class Classifier
    {
        [XmlElement("Ид")]
        public string Id { get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Группы
        /// </summary>
        [XmlArray("Группы")]
        [XmlArrayItem("Группа", typeof(Group))]
        public List<Group> Groups { get; set; }

        /// <summary>
        /// Категории
        /// </summary>
        [XmlArray("Категории")]
        [XmlArrayItem("Категория", typeof(Category))]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Справочники
        /// </summary>
        [XmlArray("Свойства")]
        [XmlArrayItem("Свойство", typeof(Property))]
        public List<Property> Properties { get; set; }
    }
}