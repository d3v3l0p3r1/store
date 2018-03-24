using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Components
{
    public class ImageEdit : ViewComponent
    {
        public ImageEdit()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(int? fileID, string fileInputID)
        {
            return View("ImageEdit", new ImageEditModel { FileID = fileID });
        }

    }

    public class ImageEditModel
    {
        public int? FileID { get; set; }

        public string FileInputID { get; set; }
    }
}
