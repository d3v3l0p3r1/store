using BaseCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Admin.Users;

namespace WebApi.Mappings.Users
{
    public class UsersMappings : AutoMapper.Profile
    {
        public UsersMappings()
        {
            CreateMap<User, UserDetailDto>();
            CreateMap<UserDetailDto, User>();

            
        }
    }
}
