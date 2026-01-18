using Auth.Domain.Users;
using Microsoft.EntityFrameworkCore;


namespace Auth.Infrastructure.Context.Domain
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(DomainDbContext).Assembly);
        }

    }
}
