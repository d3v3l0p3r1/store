using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base.Entities;
using Base.Services;
using Magazin.Helpers;

namespace Magazin.Controllers
{
    public class FileController : BaseController
    {
        private readonly IFileSystemService _fileSystemService;
        public FileController(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public JsonNetResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            var fileDatas = new List<FileData>();

            foreach (var pf in files)
            {
                var fd = _fileSystemService.Save(pf.FileName, pf.InputStream, pf.ContentLength);
                fileDatas.Add(fd);                
            }

            return new JsonNetResult(fileDatas);
        }


    }
}