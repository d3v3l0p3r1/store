using System;
using BaseCore.DAL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCore.DAL.Implementations.Entities
{
    /// <summary>
    /// История синхронизаций с 1С
    /// </summary>
    public class OneAssSync : BaseEntity
    {
        /// <summary>
        /// Дата синхронизации
        /// </summary>
        public DateTime DateTime { get; set; }
    }

    public class OneAssSyncConfiguration : IEntityTypeConfiguration<OneAssSync>
    {
        public void Configure(EntityTypeBuilder<OneAssSync> builder)
        {
            
        }
    }
}
