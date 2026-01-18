
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Infrastructure.Configurations.RabbitMQ
{
    public static class RabbitMqConfig
    {
        public static IServiceCollection ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

            services.AddSingleton<RabbitMqConnectionHolder>();

            services.AddHostedService<RabbitMqHostedService>();

            return services;
        }
    }
}
