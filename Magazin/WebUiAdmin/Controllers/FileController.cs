using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace WebUiAdmin.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile()
        {
            var reader = new MultipartReader(Request.GetMultipartBoundary(), HttpContext.Request.Body);
            FileMultipartSection filePart;
            bool isFileSection = false;

            do
            {
                var section = await reader.ReadNextSectionAsync();
                if (section == null)
                {
                    throw new Exception();
                }

                filePart = section.AsFileSection();

                if (filePart != null)
                {
                    isFileSection = true;
                }

            } while (isFileSection == false);

            var fileData = _fileService.SaveFile(filePart.FileName, filePart.FileStream);
            return new JsonResult(fileData);
        }

        public IActionResult GetFilePath(int id)
        {            
            return new JsonResult(_fileService.GetFilePath(id));
        }

        public IActionResult GetFile(int id)
        {
            if (id == 0)
            {
                return NoContent();
            }

            var path = _fileService.GetVirtualPath(id);

            if (path == null)
            {
                return NotFound();
            }

            return File(path, "image/png");
        }
    }
}