using System.ComponentModel.DataAnnotations;

namespace BaseCore.DAL.Implementations.Entities.Documents
{
    /// <summary>
    /// Статус документа
    /// </summary>
    public enum DocumentStatus
    {
        /// <summary>
        /// Новый
        /// </summary>
        [Display(Name = "Новый")]
        New = 0,

        /// <summary>
        /// Проведен
        /// </summary>
        [Display(Name = "Проведен")]
        Processed = 10,

        /// <summary>
        /// Отменен
        /// </summary>
        [Display(Name = "Отменен")]
        Canceled = 20
    }
}
