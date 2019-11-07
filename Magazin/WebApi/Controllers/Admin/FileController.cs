using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Зашрузка изображений
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fileService"></param>
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(FileData), 200)]
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

            var fileData = await _fileService.SaveFile(filePart.FileName, filePart.FileStream);
            return new JsonResult(fileData);
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetFilePath(int id)
        {
            var result = await _fileService.GetFilePath(id);
            return new JsonResult(result);
        }

        /// <summary>
        /// Get fileinfo
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFile(int id)
        {
            if (id == 0)
            {
                return NoContent();
            }

            var path = await _fileService.GetVirtualPath(id);

            if (path == null)
            {
                return NotFound();
            }

            return File(path, "image/png");
        }
    }
}