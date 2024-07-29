using IdentityServiceContract.Commands;
using IdentityServiceContract.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Platform.RabbitMq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Api.Account;

namespace WebApi.Controllers
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
        private readonly ICommandPublisher<LoginCommand, LoginDto> _loginCommandPublisher;

        public AccountController(ICommandPublisher<LoginCommand, LoginDto> loginCommandPublisher)
        {
            _loginCommandPublisher = loginCommandPublisher;
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
            var result =  await _loginCommandPublisher.Send(new LoginCommand { Login = model.Login, Password = model.Password });

            return Ok(result);
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