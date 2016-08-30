using System.Security.Claims;
using System.Threading.Tasks;
using Base.Security.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Claim = Base.Security.Entities.Claim;

namespace Base.Security.Entities
{
    public class User : IdentityUser<int, Login, UserRole, Claim>
    {


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
    }
}
