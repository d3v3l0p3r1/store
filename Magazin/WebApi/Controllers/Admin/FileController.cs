using System;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers.Admin
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
        private readonly ILogger<FileController> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fileService"></param>
        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(FileData), 200)]
        public async Task<IActionResult> SaveFile(IFormFile formFile)
        {
            using (var stream = formFile.OpenReadStream())
            {
                var fileData = await _fileService.SaveFile(formFile.FileName, stream);
                return new JsonResult(fileData);
            }
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

            try
            {
                var path = await _fileService.GetVirtualPath(id);
                if (path == null)
                {
                    return NotFound();
                }
                return File(path, "image/png");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"GetFileError: {id}");
                return NotFound();
            }
        }
    }
}