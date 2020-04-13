using BaseCore.Security.Entities;
using Microsoft.AspNetCore.Identity;

namespace BaseCore.Security.Services.Abstract
{
    public interface IUserRepository : IUserStore<User>
    {
        IUserRepository Create();
    }

  
}