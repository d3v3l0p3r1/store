using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazin.Models
{
    public class DetailViewModel
    {
        public string Id { get; }
        public int? EntityId { get; set; }
        public int? CategoryId { get; set; }
        public bool ReadOnly { get; set; }

        public DetailViewModel()
        {
            Id = "detail_" + Guid.NewGuid().ToString("N");
        }

    }
}