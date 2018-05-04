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
    public class UserService : IUserService
    {
        private readonly UserManager _userManager;

        public UserService(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<User> GetAll()
        {
            return _userManager.Users.Where(x=> x.Hidden == false);
        }

        public User Find(int id)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Update(User entity)
        {
            _userManager.UpdateAsync(entity);

            return entity;
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x=>x.Id == id);

            if (user == null)
            {
                return;
            }

            user.Hidden = true;
            _userManager.UpdateAsync(user);
        }

        public User Create(User entity)
        {   
            throw new NotImplementedException();
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
