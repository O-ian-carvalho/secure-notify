using Auth.Domain.Common.Interfaces;
using Auth.Domain.Users.Adapters;
using Auth.Domain.Users.Repositories;
using Auth.Infrastructure.Adapters;
using Auth.Infrastructure.Context.Domain;
using Auth.Infrastructure.Context.Identity;
using Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;

namespace Auth.Infrastructure.Configuration
{
    public static class ConfigureInfra
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbConnection>(sp =>
            {
                var connection = new NpgsqlConnection(
                    configuration.GetConnectionString("DefaultConnection")
                );

                connection.Open();
                return connection;
            });


            services.AddDbContext<DomainDbContext>((sp, options) =>
            {
                options.UseNpgsql(sp.GetRequiredService<DbConnection>());
            });

            services.AddDbContext<AuthDbContext>((sp, options) =>
            {
                options.UseNpgsql(sp.GetRequiredService<DbConnection>());
            });


     

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<DomainDbContext>();
            services.AddScoped<AuthDbContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>();

            return services;
        }
    }
}
