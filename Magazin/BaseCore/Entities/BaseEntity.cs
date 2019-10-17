using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCore.Entities
{    
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        public bool Hidden { get; set; }
    }
}
