using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Dtos;
using Notifications.Application.UseCases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Notifications.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailDataDto emailData)
        {
            var email = User.FindFirstValue(JwtRegisteredClaimNames.Email);
            await _mediator.Send(new SendEmailCommand(emailData.Subject, emailData.Body, email, emailData.To));
            return NoContent();
        }
    }
}
