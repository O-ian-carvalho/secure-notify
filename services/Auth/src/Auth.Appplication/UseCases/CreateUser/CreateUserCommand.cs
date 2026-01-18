using MediatR;

namespace Auth.Application.UseCases.CreateUser
{
    public record CreateUserCommand(string Email, string Password) : IRequest<Unit>;
}
