using AutoMapper;
using BaseCore.Security.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers.Admin.Documents
{
    /// <summary>
    /// Base oducment controller
    /// </summary>
    /// <typeparam name="TService">Сервис</typeparam>
    /// <typeparam name="TEntity">Сущность</typeparam>
    /// <typeparam name="TEntry">Табличная часть</typeparam>
    /// <typeparam name="TDetail">Детальное представление</typeparam>
    /// <typeparam name="TList">Лист</typeparam>
    [Authorize(Roles = "admin")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseDocumentController<TService, TEntity, TEntry, TDetail, TList> : ControllerBase
        where TService : IBaseDocumentService<TEntity, TEntry>
        where TEntity : BaseDocument<TEntry>
        where TEntry : BaseDocumentEntry
        where TDetail : class
        where TList : class
    {
        private TService _service;
        private IMapper _mapper;
        private UserManager _userManager;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="mapper"></param>
        public BaseDocumentController(TService service, IMapper mapper, UserManager userManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Получить документ прихода
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<TDetail>> Get(long id)
        {
            var entity = await _service.GetAsync(id);
            var result = _mapper.Map<TDetail>(entity);
            return Ok(result);
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
        public virtual async Task<ActionResult<ListRespone<TList>>> GetAll(int take = 20, int page = 1, DocumentStatus? state = null)
        {
            if (take < 1)
            {
                return BadRequest("Ограничение должно быть больше 0");
            }

            if (page < 1)
            {
                return BadRequest("Страница не может быть меньше 1");
            }

            var all = _service.GetAllAsNotracking().Include(x => x.Author).AsNoTracking();

            if (state != null)
            {
                all = all.Where(x => x.DocumentStatus == state.Value);
            }


            var skip = (page - 1) * take;
            var total = await all.CountAsync();
            var orders = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            var result = new ListRespone<TList>()
            {
                Data = orders.Select(x => _mapper.Map<TList>(x)).ToList(),
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
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public virtual async Task<ActionResult<TEntity>> Create(TDetail document)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var entity = _mapper.Map<TEntity>(document);
                entity.AuthorId = user.Id;
                await _service.CreateAsync(entity);
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
        public async Task<IActionResult> Update(TDetail document)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(document);
                await _service.UpdateAsync(entity);
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
                await _service.DeleteAsync(id);
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
                await _service.Apply(id);
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
                await _service.Discard(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.GetBaseException().Message);
            }
        }
    }
}
