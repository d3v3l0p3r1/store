namespace WebApi.Models.Admin.Exchange
{
    /// <summary>
    /// Модель создания категории
    /// </summary>
    public class CategoryRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        public string ParentId { get; set; }
    }
}
