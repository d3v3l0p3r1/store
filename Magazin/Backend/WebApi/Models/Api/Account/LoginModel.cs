namespace WebApi.Models.Api.Account
{
    /// <summary>
    /// Модель входа пользователя
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
