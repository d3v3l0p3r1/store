using IdentityServiceContract.Commands;
using IdentityServiceContract.Dto;
using Platform.RabbitMq;

namespace IdentityServiceContract.Publishers
{
    public class CreateUserCommandPublisher : CommandPublisher<CreateUserCommand, UserDto>
    {
        public CreateUserCommandPublisher(RabbitMqOptions options) : base(options)
        {
        }
    }
}
