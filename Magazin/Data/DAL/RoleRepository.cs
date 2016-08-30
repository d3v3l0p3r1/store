using Base.Security.Entities;
using Base.Security.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.DAL
{
    public class RoleRepository : RoleStore<Role, int, UserRole>, IRoleRepository
    {
        public RoleRepository(DataContext context) : base(context)
        {
        }
    }
}
