<<<<<<< HEAD:Magazin/BaseCore.Security/Entities/User.cs
﻿namespace BaseCore.Security.Entities
=======
﻿
using BaseCore.DAL.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BaseCore.Security.Entities
>>>>>>> 703143d03ef44b8f5666e74dce4a64271aa0157c:Magazin/Backend/BaseCore.Security/Entities/User.cs
{
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Hidden
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Тип пользователя
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Пользователь админки
        /// </summary>
        User = 0,

        /// <summary>
        /// Пользователь магазина
        /// </summary>
        PublicUser = 10,
    }
}
