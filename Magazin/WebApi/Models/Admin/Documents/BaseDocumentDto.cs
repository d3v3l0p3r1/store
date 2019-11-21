using DataCore.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Admin.Documents
{
    /// <summary>
    /// Базовое представление документа
    /// </summary>
    public abstract class BaseDocumentDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Дата проводки
        /// </summary>
        public DateTime? ProcessDate { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public DocumentStatus DocumentStatus { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public long? AuthorId { get; set; }

        /// <summary>
        /// Автор документа
        /// </summary>
        public Lookup Author { get; set; }
    }
}
