using Platform.RabbitMq;

namespace IdentityServiceContract.Commands
{
    public class LoginCommand : ICommand
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
