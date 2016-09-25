using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magazin.Controllers
{
    public class FileController : BaseController
    {
        public JsonResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var httpPostedFileBase in files)
            {

                httpPostedFileBase.SaveAs();
            }
        }
    }
}