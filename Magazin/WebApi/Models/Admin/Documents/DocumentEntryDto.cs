namespace WebApi.Models.Admin.Documents
{
    /// <summary>
    /// Табличная часть документа
    /// </summary>
    public class DocumentEntryDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        public Lookup Product { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
    }
}
