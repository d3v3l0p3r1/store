using System;
<<<<<<< HEAD:Magazin/BaseCore/Entities/FileData.cs
=======
using BaseCore.DAL.Abstractions;
>>>>>>> 703143d03ef44b8f5666e74dce4a64271aa0157c:Magazin/Backend/DataCore/Entities/FileData.cs

namespace BaseCore.DAL.Implementations.Entities
{
    public class FileData
    {
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
