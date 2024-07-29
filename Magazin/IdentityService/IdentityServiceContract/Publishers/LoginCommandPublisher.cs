using IdentityServiceContract.Commands;
using IdentityServiceContract.Dto;
using Platform.RabbitMq;

namespace IdentityServiceContract.Publishers
{
    public class LoginCommandPublisher : CommandPublisher<LoginCommand, LoginDto>
    {
        public LoginCommandPublisher(RabbitMqOptions options) : base(options)
        {
        }
    }
}
