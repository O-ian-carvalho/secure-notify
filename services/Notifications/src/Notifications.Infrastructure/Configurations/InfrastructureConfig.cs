using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Configurations.RabbitMQ;

namespace Notifications.Infrastructure.Configurations
{
    public static class InfrastructureConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRabbitMq(configuration);
            return services;
        }
    }
}
