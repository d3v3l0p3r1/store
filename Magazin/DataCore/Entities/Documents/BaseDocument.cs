using BaseCore.Entities;
using BaseCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public abstract class BaseDocument<TEntry> : BaseEntity where TEntry: BaseDocumentEntry
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime? ProcessDate { get; set; }

        /// <summary>
        /// Статус документа
        /// </summary>
        public DocumentStatus DocumentStatus { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Табличная часть
        /// </summary>
        public List<TEntry> Entries { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public long AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public User Author { get; set; }


    }
}
