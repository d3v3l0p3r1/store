﻿
using BaseCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace BaseCore.Security.Entities
{
    public class User : IdentityUser<long>, IBaseEntity
    {
        
        public bool Hidden { get; set; }

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Тип пользователя (полльзователь, компания)
        /// </summary>
        public UserType Type { get; set; }
    }

    /// <summary>
    /// Тип пользователя
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Физическое лицо
        /// </summary>
        User = 0,

        /// <summary>
        /// Компания
        /// </summary>
        Company = 10
    }
}
