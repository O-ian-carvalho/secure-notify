using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Users.Adapters
{
    public interface IIdentityService
    {
        Task<Guid> CreateUserAsync(string email, string password);
        Task<bool> CheckPasswordAsync(string email, string password);
    }
}
