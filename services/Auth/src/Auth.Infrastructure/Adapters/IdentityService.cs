using Auth.Domain.Users.Adapters;
using Microsoft.AspNetCore.Identity;


namespace Auth.Infrastructure.Adapters
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Guid> CreateUserAsync(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errors}");
            }

            return Guid.Parse(user.Id);
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            return await _userManager.CheckPasswordAsync(user, password);
        }

    }
}
