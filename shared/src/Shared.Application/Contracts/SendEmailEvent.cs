

namespace Shared.Application.Contracts
{
    public record SendEmailEvent(
           string Title,
           string Body,
           string Sender,
           List<string> Receivers
       );
}
