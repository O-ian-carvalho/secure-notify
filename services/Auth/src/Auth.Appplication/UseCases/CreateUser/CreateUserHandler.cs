using Shared.Application.Exceptions;
using Auth.Domain.Common.Interfaces;
using Auth.Domain.Users;
using Auth.Domain.Users.Adapters;
using Auth.Domain.Users.Repositories;
using MediatR;

namespace Auth.Application.UseCases.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler( IUserRepository userRepository, IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _identityService = identityService;
           _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var validationResult = new CreateUserCommandValidator().Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (await _userRepository.GetByEmailAsync(request.Email) != null)
                throw new ConflictException("Email already in use.");
            
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
               
                Guid userId = await _identityService.CreateUserAsync(request.Email, request.Password);

                var user = new User(
                    userId,
                    new Email(request.Email)
                );

                await _userRepository.AddAsync(user);


            }, cancellationToken);
            
            return Unit.Value;
        }
    }
}
