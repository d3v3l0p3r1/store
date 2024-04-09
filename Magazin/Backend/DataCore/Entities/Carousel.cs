using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    public class Carousel : BaseEntity<long>
    {
        public long FileId { get; set; }
        public FileData File { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public string Href { get; set; }

        public bool Show { get; set; }

    }
}
