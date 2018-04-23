using System;
using System.Linq;
using BaseCore.Entities;
using BaseCore.Services;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using DataCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{    
    public class BaseController<T> : Controller where T: IBaseEntity, new()
    {
        private readonly IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        } 

        public virtual IActionResult Edit(int id)
        {
            var cat = _baseService.Find(id);
            return View(cat);
        }

        [HttpPost]
        public virtual IActionResult Edit(T entity)
        {
            try
            {
                _baseService.Update(entity);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public virtual IActionResult Create()
        {
            var kind = new T();

            return View("Edit", kind);
        }

        [HttpPost]
        public virtual IActionResult Create(T entity)
        {
            _baseService.Update(entity);
            return View("Index");
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            _baseService.Delete(id);
            return View("Index");
        }
    }
}