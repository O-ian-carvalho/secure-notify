namespace Auth.Domain.Users.Adapters
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
