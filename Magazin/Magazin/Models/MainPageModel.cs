using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazin.Models
{
    public class MainPageModel
    {
        public List<ProductCategoryDTO> ProductCategories = new List<ProductCategoryDTO>();
        public int CurrentCategory { get; set; }
    }
}