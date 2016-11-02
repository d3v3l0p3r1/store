﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazin.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string File { get; set; } = "no-image";
        public decimal Price { get; set; }
    }
}