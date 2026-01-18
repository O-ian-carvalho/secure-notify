using Auth.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Application.Exceptions;

namespace SharedApi
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (status, message) = ex switch
            {
                ValidationException => (400, ex.Message),
                ConflictException => (409, ex.Message),
                DomainException => (422, ex.Message),
                BusinessException => (400, ex.Message),
                NotFoundException => (404, ex.Message),
                UnauthorizedAccessException => (401, ex.Message),
                _ => (500, "Internal server error")
            };

            context.Response.StatusCode = status;
            return context.Response.WriteAsJsonAsync(new { error = message });
        }
    }
    
}
