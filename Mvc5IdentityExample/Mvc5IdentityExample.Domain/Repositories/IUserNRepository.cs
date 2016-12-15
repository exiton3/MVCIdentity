using Mvc5IdentityExample.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Mvc5IdentityExample.Domain.Repositories
{
    public interface IUserNRepository : IRepositoryAsync<UserN>
    {
        UserN FindByUserName(string username);
        Task<UserN> FindByUserNameAsync(string username);
        Task<UserN> FindByUserNameAsync(CancellationToken cancellationToken, string username);
    }
}
