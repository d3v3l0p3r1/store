namespace WebApi.Models.Api.Account
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
