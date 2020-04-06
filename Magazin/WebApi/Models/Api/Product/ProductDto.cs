namespace WebApi.Models.Api.Product
{
    /// <summary>
    /// Product list model
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// File data
        /// </summary>
        public long? FileId { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Вид
        /// </summary>
        public string Kind { get; set; }
    }
}
