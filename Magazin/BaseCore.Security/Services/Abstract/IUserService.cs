using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Services;
using BaseCore.Services.Abstract;

namespace BaseCore.Security.Services.Abstract
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> CreateWithPassword(User user, string password);
        Task<User> CreateAsync(User user, string password, string role);
        Task<User> CreateAsyncWithRole(User entity, string role);
        Task<List<string>> GetRoles();
        Task<List<string>> GetRoles(long id);
        Task<User> UpdateAsync(User entity, string password, string role);
    }
}