using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using BaseCore.Security.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebUiAdmin.Models;
using WebUiAdmin.Models.Api.Account;

namespace WebUiAdmin.Controllers.Api
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Token(LoginModel model)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("Не удалось войти");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (!result.Succeeded)
            {
                return BadRequest("Неправильное имя пользователя или пароль");
            }

            var token = await GetToken(user);
            var response = new
            {
                access_token = token,
                name = user.FullName,
                email = user.Email,
                phone = user.PhoneNumber,
                address = user.Address
            };

            return Ok(response);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model.Password != model.PasswordConfirm)
            {
                return BadRequest("Пароли не совпаддают");
            }

            var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                return BadRequest("Пользователь уже существует");
            }

            user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.Name,
                Address = model.Address,
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");

                user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

                var token = await GetToken(user);

                var response = new
                {
                    access_token = token,
                    name = user.FullName,
                    email = user.Email,
                    address = user.Address,
                    phone = user.PhoneNumber
                };

                return Ok(response);
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }

        /// <summary>
        /// Получить информацию пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Info()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            var result = new
            {
                Roles = roles,
                Name = user.UserName,
                Avatar = "",
                Introduction = ""
            };

            return Ok(result);
        }

        private async Task<string> GetToken(User user)
        {
            var claims = await _signInManager.ClaimsFactory.CreateAsync(user);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims.Claims,
                expires: now.AddDays(AuthOptions.LIFETIME),
                notBefore: now,
                signingCredentials: new SigningCredentials(AuthOptions.GetKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}