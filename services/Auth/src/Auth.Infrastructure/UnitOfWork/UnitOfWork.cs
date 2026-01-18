using Auth.Domain.Common.Interfaces;
using Auth.Infrastructure.Context.Domain;
using Auth.Infrastructure.Context.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Auth.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(DomainDbContext context, AuthDbContext authDbContext)
        {
            _domainContext = context;
            _authDbContext = authDbContext;
        }

        private readonly DomainDbContext _domainContext;
        private readonly AuthDbContext _authDbContext;

        public async Task CommitAsync(CancellationToken ct)
        {
            await _domainContext.SaveChangesAsync(ct);
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken ct)
        {
            await using var transaction = await _domainContext.Database.BeginTransactionAsync(ct);

            _authDbContext.Database.UseTransaction(transaction.GetDbTransaction());

            try
            {
                await action();
                await _domainContext.SaveChangesAsync();
                await transaction.CommitAsync(ct);
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
