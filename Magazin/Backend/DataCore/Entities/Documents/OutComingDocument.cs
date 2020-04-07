using System.ComponentModel.DataAnnotations;

namespace BaseCore.DAL.Implementations.Entities.Documents
{
    /// <summary>
    /// Документ списания
    /// </summary>
    public class OutComingDocument : BaseDocument<OutComingDocumentEntry>
    {
        /// <summary>
        /// Тип списания
        /// </summary>
        public OutComingDocumentType OutComingDocumentType { get; set; }
    }

    /// <summary>
    /// Типы списания
    /// </summary>
    public enum OutComingDocumentType
    {
        [Display(Name = "Брак")]
        Defect = 0,

        [Display(Name = "Испорчен")]
        Spoiled = 10,

        [Display(Name = "Возврат")]
        Return = 20
    }

    public class OutComingDocumentEntry : BaseDocumentEntry
    {
        public OutComingDocument Document { get; set; }
    }
}
