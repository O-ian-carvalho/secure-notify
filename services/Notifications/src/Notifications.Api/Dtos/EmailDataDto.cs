namespace Notifications.Api.Dtos
{
    public record EmailDataDto(
       string Subject,
       string Body,
       List<string> To
   );
}
