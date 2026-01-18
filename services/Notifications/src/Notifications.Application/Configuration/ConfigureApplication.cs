
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Application.Configuration
{
    public static class ConfigureApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssembly(typeof(ConfigureApplication).Assembly));

            return services;
        }
    }
}
