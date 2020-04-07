using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Entities;

namespace BaseCore.Security.Services.Abstract
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user, string password, string role);
        Task<List<string>> GetRoles();
        Task<List<string>> GetRoles(long id);
        Task<User> UpdateAsync(User entity, string password, string role);
        IQueryable<User> GetAllAsNotracking();
        Task<User> GetAsync(long id);
    }
}