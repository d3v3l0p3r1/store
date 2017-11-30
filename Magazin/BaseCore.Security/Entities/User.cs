﻿
using BaseCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace BaseCore.Security.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public bool Hidden { get; set; }
    }
}