using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Components
{
    public class GridToolbarComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(string parent)
        {
            return View("GridToolbar", new GridToolbarModel(parent));
        }
    }


    public class GridToolbarModel
    {
        public string ParentGrid { get; set; }

        public GridToolbarModel(string parent)
        {
            ParentGrid = parent;
        }
    }
}
