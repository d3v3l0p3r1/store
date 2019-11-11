using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities.Documents;
using Microsoft.AspNetCore.Http;
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