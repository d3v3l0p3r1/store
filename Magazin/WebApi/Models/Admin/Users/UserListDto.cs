﻿using BaseCore.Security.Entities;

namespace WebApi.Models.Admin.Users
{
    /// <summary>
    /// Представление пользователя
    /// </summary>
    public class UserListDto
    {
        /// <summary>
        /// ИДентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

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

        /// <summary>
        /// Телефон
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
