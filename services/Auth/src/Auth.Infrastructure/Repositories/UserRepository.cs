using Auth.Domain.Users;
using Auth.Domain.Users.Repositories;
using Auth.Infrastructure.Context.Domain;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DomainDbContext _context;

        public UserRepository(DomainDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByIdAsync(Guid Id)
        {
            return await _context.Users
                .FindAsync(Id);  
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var emailVO = new Email(email);

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailVO);
        }
    }
}
