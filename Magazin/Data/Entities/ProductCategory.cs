using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;
using Linq.PropertyTranslator.Core;

namespace Data.Entities
{
    public class ProductCategory : BaseEntity
    {       
        public string Title { get; set; }        

        public int? FileId { get; set; }
        public virtual FileData File { get; set; }
    }


}
