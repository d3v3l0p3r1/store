using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(250, MinimumLength = 1)]
        [Display(Name = "Наименование")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public long? FileId { get; set; }
        [Display(Name = "Файл")]
        public FileData File { get; set; }

        public long CategoryId { get; set; }

        [Display(Name = "Категория")]
        public virtual ProductCategory Category { get; set; }

        public long? KindId { get; set; }
        [DisplayName("Вид")]
        public virtual ProductKind Kind { get; set; }

        public List<ProductImage> Images { get; set; }

        public long BrandId { get; set; }
        public Brand Brand { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string VenderCode { get; set; }

        /// <summary>
        /// Единица измерений
        /// </summary>
        public string MeasureUnit { get; set; }

        /// <summary>
        /// Внешний ключ
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Дата последнего обновления
        /// </summary>
        public DateTimeOffset UpdateTime { get; set; }

        /// <summary>
        /// Дата Создания
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }

        public long? PackageId { get; set; }
        public Package Package { get; set; }
    }

    public class ProductImage : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long FileId { get; set; }
        public FileData File { get; set; }
    }
}
