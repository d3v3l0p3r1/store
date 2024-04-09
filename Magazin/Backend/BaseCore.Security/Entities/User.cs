
using BaseCore.DAL.Abstractions;

namespace BaseCore.Security.Entities
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
