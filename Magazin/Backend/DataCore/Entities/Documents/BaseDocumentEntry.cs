using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities.Documents
{
    public abstract class BaseDocumentEntry: BaseEntity<long>
    {
        public long DocumentId { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
