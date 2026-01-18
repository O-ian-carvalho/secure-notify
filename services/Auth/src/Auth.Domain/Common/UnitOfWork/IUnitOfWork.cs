using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken ct);
        Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken ct);
    }
}
