using System.ComponentModel.DataAnnotations;

namespace BaseCore.DAL.Abstractions
{    
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        public bool Hidden { get; set; }
    }
}
