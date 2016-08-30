using Base.Security.Entities;
using Microsoft.AspNet.Identity;

namespace Base.Security.Services
{
    public interface IUserRepository : IUserStore<User, int>
    {
        IUserRepository Create();

    }

  
}