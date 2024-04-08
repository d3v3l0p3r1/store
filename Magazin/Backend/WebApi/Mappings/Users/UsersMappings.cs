using BaseCore.Security.Entities;
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
