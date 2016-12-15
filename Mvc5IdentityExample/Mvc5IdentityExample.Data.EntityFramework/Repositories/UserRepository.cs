using Mvc5IdentityExample.Domain.Entities;
using Mvc5IdentityExample.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc5IdentityExample.Data.EntityFramework.Repositories
{
    internal class UserRepository : RepositoryAsync<UserN>, IUserRepository
    {
        internal UserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public UserN FindByUserName(string username)
        {
            return Set.FirstOrDefault(x => x.UserName == username);
        }

        public Task<UserN> FindByUserNameAsync(string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<UserN> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }
    }
}