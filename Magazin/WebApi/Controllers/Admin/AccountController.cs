using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUiAdmin.Models.AccountViewModels;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Авторизация, Аутентификация
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="signInManager"></param>
        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }



    }
}