using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Services.Abstract;
using DataCore.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var users = await _userService.GetAllAsync(uow);
                return new JsonResult(users);
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}