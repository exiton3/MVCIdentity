using Mvc5IdentityExample.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mvc5IdentityExample.Data.EntityFramework.Repositories
{
    internal class RepositoryAsync<TEntity> : Repository<TEntity>, IRepositoryAsync<TEntity> where TEntity : class
    {
        internal RepositoryAsync(UnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Set.ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> PageAllAsync(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public Task<TEntity> FindByIdAsync(object id)
        {
            return Set.FindAsync(id);
        }

        public Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id)
        {
            return Set.FindAsync(cancellationToken, id);
        }
    }
}