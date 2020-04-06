using System;
using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    public class FileData : BaseEntity
    {
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
