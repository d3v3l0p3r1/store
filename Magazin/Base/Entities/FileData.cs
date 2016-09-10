using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities
{
    public class FileData : BaseEntity
    {
        public FileData()
        {
            CreationDate = DateTime.Now;
            ChangeDate = DateTime.Now;
        }

        
        public Guid FileID { get; set; }        
        public string FileName { get; set; }        
        public long Size { get; set; }
        public string Extension { get; set; }        
        public DateTime CreationDate { get; set; }        
        public DateTime ChangeDate { get; set; }
    }
}
