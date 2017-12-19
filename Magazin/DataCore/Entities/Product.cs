using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class Product : BaseEntity
    {
        [Display(Name = "Наименование")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public int? FileID { get; set; }

        [Display(Name = "Файл")]
        public virtual FileData File { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }

        [Display(Name = "Категория")]
        public virtual ProductCategory Category { get; set; }
    }
}
