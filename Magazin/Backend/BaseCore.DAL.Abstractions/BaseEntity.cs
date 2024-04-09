using System.ComponentModel.DataAnnotations;

namespace BaseCore.DAL.Abstractions
{    
    public abstract class BaseEntity<T> : IBaseEntity<T> where T : struct
    {
        [Key]
        public T Id { get; set; }
        public bool Hidden { get; set; }
    }
}
