using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    /// <summary>
    /// Упаковка
    /// </summary>
    public class Package : BaseEntity<long>
    {
        public string ExternalId { get; set; }

        public string Value { get; set; }
    }
}
