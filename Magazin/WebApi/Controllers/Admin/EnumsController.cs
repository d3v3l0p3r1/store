using System.Collections.Generic;
using BaseCore.DAL.Implementations.Entities.Documents;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Для работы с перечислениями
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [ResponseCache(Duration = 1600)]
    [ProducesResponseType(typeof(Dictionary<int, string>), 200)]
    [ApiExplorerSettings(GroupName = "admin")]

    public class EnumsController : ControllerBase
    {
        /// <summary>
        /// Получить статусы документов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetDocumentStatuses()
        {

            var dict = EnumExtensions.GetEnumDesciption<DocumentStatus>();
            return Ok(dict);
        }
    }
}