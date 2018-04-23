using System;
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
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) :base(userService)
        {
            _userService = userService;
        }

        [Route("GetUsers")]
        [Produces("application/json")]
        public IActionResult GetUsers()
        {

            var users = _userService.GetAll();


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

        public IActionResult Index()
        {
            return View();
        }

       
    }
}