using Microsoft.AspNetCore.Identity;

namespace IdentityServiceBase.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public bool Hidden { get; set; }
    }
}
