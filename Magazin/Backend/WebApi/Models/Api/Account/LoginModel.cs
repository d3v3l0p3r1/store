namespace WebApi.Models.Api.Account
{
    /// <summary>
    /// Модель входа пользователя
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
