using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Admin.Users;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Пользователи
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ListRespone<UserListDto>), 200)]
        public async Task<IActionResult> GetAll(int take = 10, int page = 1, UserType? type = null)
        {
            if (take < 1)
            {
                take = 10;
            }
            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * take;

            var users = _userService.GetAllAsNotracking();

            if (type != null)
            {
                users = users.Where(x => x.Type == type.Value);
            }

            users = users.Skip(skip).Take(take);

            var result = await users.Select(x => new UserListDto()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Type = x.Type
            }).ToListAsync();

            var total = await users.CountAsync();

            var response = new ListRespone<UserListDto>()
            {
                Data = result,
                Total = total
            };

            return Ok(response);
        }

        /// <summary>
        /// Получить инфу по пользователю
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var user = await _userService.GetAsync(id);
            var role = await _userService.GetRoles(id);
            var model = _mapper.Map<UserDetailDto>(user);
            model.Role = role.First();

            return Ok(model);
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch(UserDetailDto userDetailDto)
        {
            var user = _mapper.Map<User>(userDetailDto);

            await _userService.UpdateAsync(user, userDetailDto.Password, userDetailDto.Role);

            return Ok();
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserDetailDto userDetailDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDetailDto);

                await _userService.CreateAsync(user, userDetailDto.Password, userDetailDto.Role);

                return Ok(user);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }            
        }

        /// <summary>
        /// Все роли
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userService.GetRoles();

            return Ok(roles);
        }


    }
}