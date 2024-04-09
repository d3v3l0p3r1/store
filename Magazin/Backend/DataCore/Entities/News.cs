using System;
using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    public class News : BaseEntity<long>
    {
        public int? ImageID { get; set; }
        public virtual FileData Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool ShowOnSlider { get; set; }
    }
}
