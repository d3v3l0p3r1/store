using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Admin.Documents;
using WebApi.Models.Admin.Documents.IncomingDocuments;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Документы прихода
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    public class IncomingDocumentController : ControllerBase
    {
        private readonly IIncomingDocumentService incomingDocumentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="incomingDocumentService"></param>
        /// <param name="mapper"></param>
        public IncomingDocumentController(IIncomingDocumentService incomingDocumentService, IMapper mapper)
        {
            this.incomingDocumentService = incomingDocumentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить документ прихода
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IncomingDocumentDetailDto), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var entity = await incomingDocumentService.GetAsync(id);
            var result = _mapper.Map<IncomingDocumentDetailDto>(entity);
            return Ok(entity);
        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="take"></param>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<IncomingDocumentListDto>), 200)]
        public async Task<IActionResult> GetAll(int take = 20, int page = 1, DocumentStatus? state = null)
        {
            if (take < 1)
            {
                return BadRequest("Ограничение должно быть больше 0");
            }

            if (page < 1)
            {
                return BadRequest("Страница не может быть меньше 1");
            }

            var all = incomingDocumentService.GetAllAsNotracking();

            if (state != null)
            {
                all = all.Where(x => x.DocumentStatus == state.Value);
            }


            var skip = (page - 1) * take;
            var total = await all.CountAsync();
            var orders = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            var result = new ListRespone<IncomingDocumentListDto>()
            {
                Data = orders.Select(x => _mapper.Map<IncomingDocumentListDto>(x)).ToList(),
                Total = total
            };

            return Ok(result);
        }

        /// <summary>
        /// Создать документ
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IncomingDocument), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Create(IncomingDocumentDetailDto document)
        {
            try
            {
                var entity = _mapper.Map<IncomingDocument>(document);
                await incomingDocumentService.CreateAsync(entity);
                return Ok(entity);
            }
            catch (Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(IncomingDocument), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Update(IncomingDocumentDetailDto document)
        {
            try
            {
                var entity = _mapper.Map<IncomingDocument>(document);
                await incomingDocumentService.UpdateAsync(entity);
                return Ok(entity);
            }
            catch (Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Delete document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await incomingDocumentService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Провести документ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Apply([FromQuery]long id)
        {
            try
            {
                await incomingDocumentService.Apply(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Отменить проводку
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Discard([FromQuery]long id)
        {
            try
            {
                await incomingDocumentService.Discard(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.GetBaseException().Message);
            }
        }
    }
}