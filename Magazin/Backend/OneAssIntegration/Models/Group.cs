using System.Collections.Generic;
using System.Xml.Serialization;

namespace OneAssIntegration.Models
{
    public class Group
    {
        [XmlElement("Ид")]
        public string Id { get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }


        [XmlArray("Группы")]
        [XmlArrayItem("Группа", typeof(Group))]
        public List<Group> Childs { get; set; }
    }
}