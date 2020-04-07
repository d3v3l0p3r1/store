using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Models
{
    public class AuthOptions
    {
        public const string AUDIENCE = "http://localhost:51145/";
        const string KEY = "WAH7d3287y3252fehjkfle;#543253252w21#325326";
        public const int LIFETIME = 7;
        public const string ISSUER = "http://localhost:51145/";

        public static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
