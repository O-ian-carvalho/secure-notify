
using MediatR;

namespace Auth.Application.UseCases.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<string>;
    
}
