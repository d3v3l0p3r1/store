using System;
using System.Collections.Generic;
using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities.Documents
{
    public abstract class BaseDocument<TEntry> : BaseEntity<long> where TEntry: BaseDocumentEntry
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
    }
}
