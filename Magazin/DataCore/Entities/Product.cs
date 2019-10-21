using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Наименование")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public long? FileID { get; set; }
        [Display(Name = "Файл")]
        public FileData File { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public long CategoryId { get; set; }

        [Display(Name = "Категория")]
        public virtual ProductCategory Category { get; set; }

        public long KindId { get; set; }
        [DisplayName("Вид")]
        public virtual ProductKind Kind { get; set; }
    }
}
