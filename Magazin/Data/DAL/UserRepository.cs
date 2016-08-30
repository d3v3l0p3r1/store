using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Security.Entities;
using Base.Security.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.DAL
{
    public class UserRepository : UserStore<User, Role, int, Login, UserRole, Claim>, IUserRepository
    {
        public UserRepository(DataContext dataContext)
            : base(dataContext)
        {

        }



        public static UserRepository Create()
        {
            return new UserRepository(new DataContext());
        }

        IUserRepository IUserRepository.Create()
        {
            return Create();
        }
    }
}
