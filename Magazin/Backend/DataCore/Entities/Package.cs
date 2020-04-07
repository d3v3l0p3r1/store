using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    /// <summary>
    /// Упаковка
    /// </summary>
    public class Package : BaseEntity
    {
        public string ExternalId { get; set; }

        public string Value { get; set; }
    }
}
