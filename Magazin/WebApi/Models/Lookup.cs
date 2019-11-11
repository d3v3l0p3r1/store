using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    /// <summary>
    /// Кратское описание сущности
    /// </summary>
    public class Lookup
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Title { get; set; }

    }
}
