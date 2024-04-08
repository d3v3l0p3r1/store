using BaseCore.Security.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseCore.Security.Services.Concrete
{
    public class UserManager
    {
        private readonly DbContext _dbContext;

        public UserManager(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<User> Users => _dbContext.Set<User>();
        public DbSet<Role> Roles => _dbContext.Set<Role>();

        public async Task SetEmailAsync(User original, string email)
        {
            
        }

        internal async Task AddToRoleAsync(User original, string role)
        {
            throw new NotImplementedException();
        }

        internal async Task CreateAsync(User entity, string password)
        {
            throw new NotImplementedException();
        }

        internal async Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        internal async Task<User> FindByIdAsync(string v)
        {
            throw new NotImplementedException();
        }

        internal async Task GetRolesAsync(User user)
        {
            throw new NotImplementedException();
        }

        internal async Task RemoveFromRolesAsync(User original, List<string> roles)
        {
            throw new NotImplementedException();
        }

        internal async Task SetPhoneNumberAsync(User original, object phoneNumber)
        {
            throw new NotImplementedException();
        }

        internal async Task SetUserNameAsync(User original, string userName)
        {
            throw new NotImplementedException();
        }

        internal async Task UpdateAsync(User original)
        {
            throw new NotImplementedException();
        }
    }
}
