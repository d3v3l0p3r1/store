using Base.Security.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Base.Security.Services
{
    public interface IRoleRepository : IRoleStore<Role, int>
    {

    }

  
}
