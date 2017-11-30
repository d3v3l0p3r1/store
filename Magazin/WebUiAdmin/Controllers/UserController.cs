﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using DataCore.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUiAdmin.Models;

namespace WebUiAdmin.Controllers
{    
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, DataContext dataContext) : base(dataContext)
        {
            _userService = userService;
        }

        [Route("GetUsers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUsers()
        {
            using (var uow = CreateUnitOfWork())
            {
                var users = _userService.GetAll(uow);


                var result = users.Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.Email,
                    x.PhoneNumber
                });

                var total = users.Count();


                return new JsonResult(new
                {
                    Data = result,
                    Total = total
                });
            }
        }




        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var user = await _userService.FindAsync(uow, id);

                if (user != null)
                {
                    return PartialView(user);
                }

                return View("Error", new ErrorViewModel(){ });
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            throw new NotImplementedException();
        }
    }
}