using Auth.Api.DTOs.Login;
using Auth.Api.DTOs.Register;
using Auth.Application.UseCases.CreateUser;
using Auth.Application.UseCases.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _mediator.Send(new CreateUserCommand(request.Email, request.Password));
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(new LoginCommand(request.Email, request.Password));
            return Ok(new LoginResponse(result));
        }
    }
}
