﻿using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; set; }

        public int? ImageID { get; set; }
        public virtual FileData Image { get; set; }
    }
}
