namespace Auth.Api.DTOs.Login
{
    public record LoginRequest(
       string Email,
       string Password
    );
}
