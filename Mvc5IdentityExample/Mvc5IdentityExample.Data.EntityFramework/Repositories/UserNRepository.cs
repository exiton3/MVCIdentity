using System.Collections.Generic;
using Mvc5IdentityExample.Domain.Entities;
using Mvc5IdentityExample.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace Mvc5IdentityExample.Data.EntityFramework.Repositories
{
    internal class UserRepository : RepositoryAsync<User>, IUserNRepository
    {
        internal UserRepository(UnitOfWork unitOfWork)
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

        public IEnumerable<UserDto> GetProjected()
        {
            Set.ProjectTo<UserDto>();
            return new List<UserDto>();
        }
    }

    internal class UserDto
    {
        public string Name { get; set; }

        public string GroupName { get; set; }
    }
}