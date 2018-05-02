using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace WebUiAdmin.Models
{
    public class AuthOptions
    {
        public const string AUDIENCE = "__WEB_UI_ADMIN";
        const string KEY = "WAH7d3287y3252fehjkfle;#543253252w21#325326";
        public const int LIFETIME = 7;
        public const string ISSUER = "__WEB_TOKEN_ISSUER";

        public static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
