using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using BaseCore.Services;
using BaseCore.Services.Abstract;

namespace BaseCore.Security.Services.Concrete
{
    public class UserService : IBaseService<User>, IUserService
    {
        private readonly UserManager _userManager;

        public UserService(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<User> GetAll(IUnitOfWork uow)
        {
            return _userManager.Users.Where(x=> x.Hidden == false);
        }

        public async Task<User> FindAsync(IUnitOfWork uow, int id)
        {
            return await Task.FromResult(_userManager.Users.FirstOrDefault(x => x.Id == id));
        }

        public async Task<User> UpdateAsync(IUnitOfWork uow, User entity)
        {
             await _userManager.UpdateAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(IUnitOfWork uow, int id)
        {
            var user = _userManager.Users.FirstOrDefault(x=>x.Id == id);

            if (user == null)
            {
                return;
            }

            user.Hidden = true;
            await _userManager.UpdateAsync(user);
        }
    }
}
