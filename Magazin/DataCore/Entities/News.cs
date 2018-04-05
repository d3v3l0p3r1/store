using System;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class News : BaseEntity
    {
        public int? ImageID { get; set; }
        public virtual FileData Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool ShowOnSlider { get; set; }
    }
}
