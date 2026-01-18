using MediatR;


namespace Notifications.Application.UseCases
{
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        Task<Unit> IRequestHandler<SendEmailCommand, Unit>.Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
