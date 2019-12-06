using BaseCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities
{
    /// <summary>
    /// Brand
    /// </summary>
    public class Brand : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// FileId
        /// </summary>
        public long? FileId { get; set; }

        /// <summary>
        /// File
        /// </summary>
        public FileData File { get; set; }
    }
}
