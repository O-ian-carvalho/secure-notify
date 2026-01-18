
using Auth.Domain.Users.Adapters;
using Auth.Domain.Users.Repositories;
using MediatR;

namespace Auth.Application.UseCases.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IIdentityService _identityService;

        public LoginHandler(IUserRepository userRepository, ITokenService tokenService, IIdentityService identityService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _identityService = identityService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validPassword = await _identityService.CheckPasswordAsync(request.Email, request.Password);

            if (!validPassword)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");
            

            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
