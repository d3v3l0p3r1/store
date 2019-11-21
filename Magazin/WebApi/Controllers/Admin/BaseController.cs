using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using DataCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : IBaseEntity, new()
    {
        private readonly IBaseService<T> _baseService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseService"></param>
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }


        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<T>> Get(long id)
        {
            try
            {
                var entity = await _baseService.GetAsync(id);
                return Ok(entity);
            }
            catch(Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }
        
        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult<T>> Create(T entity)
        {
            try
            {
                await _baseService.CreateAsync(entity);
                return Ok(entity);
            }
            catch(Exception e)
            {
                return BadRequest(e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPatch]
        public virtual async Task<ActionResult<T>> Update([FromBody]T entity)
        {
            try
            {
                await _baseService.UpdateAsync(entity);
                return Ok(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await _baseService.DeleteAsync(id);
            return Ok();
        }
    }
}