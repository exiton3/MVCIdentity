using Mvc5IdentityExample.Domain.Entities;
using Mvc5IdentityExample.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc5IdentityExample.Data.EntityFramework.Repositories
{
    internal class UserNRepository : RepositoryAsync<User>, IUserNRepository
    {
        internal UserNRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public User FindByUserName(string username)
        {
            return Set.FirstOrDefault(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }
    }
}