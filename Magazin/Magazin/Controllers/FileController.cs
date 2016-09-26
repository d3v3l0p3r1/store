using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magazin.Helpers;

namespace Magazin.Controllers
{
    public class FileController : BaseController
    {


        public JsonNetResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var httpPostedFileBase in files)
            {
                Guid id = Guid.NewGuid();

                httpPostedFileBase.SaveAs(id.ToString());
            }

            return new JsonNetResult(new { ok = "ok" });
        }
    }
}