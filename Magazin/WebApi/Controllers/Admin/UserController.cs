using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using DataCore.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUiAdmin.Models;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) :base(userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUsers()
        {

            var users = _userService.GetAllAsNotracking();


            var result = await users.Select(x => new
            {
                x.Id,
                x.UserName,
                x.Email,
                x.PhoneNumber
            }).ToListAsync();

            var total = await users.CountAsync();

            return new JsonResult(new
            {
                Data = result,
                Total = total
            });

        }
    }
}