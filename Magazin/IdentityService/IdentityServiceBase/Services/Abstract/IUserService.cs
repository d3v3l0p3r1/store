using IdentityServiceBase.Entities;

namespace IdentityServiceBase.Services.Abstract
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user, string password, string role);
        Task<List<string>> GetRoles();
        Task<List<string>> GetRoles(long id);
        Task<User> UpdateAsync(User entity, string password, string role);
        Task<User?> GetAsync(long id);
        Task DeleteAsync(long id);
    }
}