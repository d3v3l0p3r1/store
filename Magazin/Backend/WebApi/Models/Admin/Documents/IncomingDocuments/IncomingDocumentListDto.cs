namespace WebApi.Models.Admin.Documents.IncomingDocuments
{
    /// <summary>
    /// Incoming document DTO
    /// </summary>
    public class IncomingDocumentListDto : BaseDocumentDto
    {
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Total { get; set; }
    }
}
