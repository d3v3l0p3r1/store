using System.ComponentModel.DataAnnotations;

namespace Base.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Hidden { get; set; }
    }
}
