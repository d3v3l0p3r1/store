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
    [Authorize(Roles = "admin")]
    public abstract class BaseController<T> : Controller where T : IBaseEntity, new()
    {
        private readonly IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        
        [HttpGet]
        public virtual async Task<IActionResult> Edit(int id)
        {
            var cat = await _baseService.GetAsync(id);
            return View(cat);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit([FromBody]T entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await _baseService.CreateAsync(entity);
                }
                else
                {
                    await _baseService.UpdateAsync(entity);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public virtual Task<IActionResult> Create()
        {
            var result = View("Edit", new T());
            return Task.FromResult((IActionResult)result);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await _baseService.DeleteAsync(id);
            return View("Index");
        }
    }
}