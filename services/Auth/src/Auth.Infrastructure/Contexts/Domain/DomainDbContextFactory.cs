using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace Auth.Infrastructure.Context.Domain
{
    public class DomainDbContextFactory : IDesignTimeDbContextFactory<DomainDbContext>
    {
        DomainDbContext IDesignTimeDbContextFactory<DomainDbContext>.CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Auth.Api");
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddEnvironmentVariables() 
                 .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DomainDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new DomainDbContext(optionsBuilder.Options);
        }
    }
}
