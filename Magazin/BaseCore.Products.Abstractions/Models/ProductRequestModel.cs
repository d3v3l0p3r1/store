namespace BaseCore.Products.Abstractions.Models
{
    /// <summary>
    /// Создание продукта
    /// </summary>
    public class ProductRequestModel
    {
        /// <summary>
        /// Внешний ключ
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ключ категории
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Ключ бренда
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string VenderCode { get; set; }

        /// <summary>
        /// Единица измерений
        /// </summary>
        public string MeasureUnit { get; set; }
    }

}
