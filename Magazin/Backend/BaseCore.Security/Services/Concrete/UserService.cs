using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCore.Security.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public UserService(UserManager userManager, RoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<User> GetAllAsNotracking()
        {
            return _userManager.Users.Where(x => x.Hidden == false);
        }

        public User Find(int id)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == id);
        }


        public async Task<User> UpdateAsync(User entity, string password, string role)
        {
            var original = await GetAsync(entity.Id);

            if (original.FullName != entity.FullName)
            {
                original.FullName = entity.FullName;
            }

            if (original.Email != entity.Email)
            {
                await _userManager.SetEmailAsync(original, entity.Email);
            }

            if (original.UserName != entity.UserName)
            {
                await _userManager.SetUserNameAsync(original, entity.UserName);
            }

            if (original.Address != entity.Address)
            {
                original.Address = entity.Address;
            }

            if (original.PhoneNumber != entity.PhoneNumber)
            {
                await _userManager.SetPhoneNumberAsync(original, entity.PhoneNumber);
            }

            if (!String.IsNullOrEmpty(role))
            {
                var roles = await GetRoles(original.Id);
                await _userManager.RemoveFromRolesAsync(original, roles);
                await _userManager.AddToRoleAsync(original, role);
            }

            await _userManager.UpdateAsync(original);

            return entity;
        }

        public void Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return;
            }

            user.Hidden = true;
            _userManager.UpdateAsync(user);
        }

        public async Task CreateAsync(User entity)
        {
            await _userManager.CreateAsync(entity);
        }

        public async Task<User> CreateAsync(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Errors.Any())
            {
                throw new Exception(String.Join(";", result.Errors.Select(x => x.Description)));
            }
            await _userManager.AddToRoleAsync(user, role);
            return user;
        }

        public async Task<User> CreateAsyncWithRole(User entity, string role)
        {
            await CreateAsync(entity);
            await _userManager.AddToRoleAsync(entity, role);
            return entity;
        }

        public Task<User> GetAsync(long id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<string>> GetRoles()
        {
            return await _roleManager.Roles.Select(x => x.Name).ToListAsync();
        }

        public async Task<List<string>> GetRoles(long id)
        {
            var user = await GetAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

    }
}
