

using MediatR;

namespace Notifications.Application.UseCases
{
    public record SendEmailCommand(
        string Title,
        string Body,
        string Sender,
        List<string> Receivers
    ) : IRequest<Unit>;
 
}
