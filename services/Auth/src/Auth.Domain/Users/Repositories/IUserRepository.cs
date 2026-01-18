
namespace Auth.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid Id);
    }
}
