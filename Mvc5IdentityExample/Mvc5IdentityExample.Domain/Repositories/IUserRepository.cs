using Mvc5IdentityExample.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Mvc5IdentityExample.Domain.Repositories
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
        User FindByUserName(string username);
        Task<User> FindByUserNameAsync(string username);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);
    }
}
