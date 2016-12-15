using Mvc5IdentityExample.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mvc5IdentityExample.Domain
{
    public interface IUnitOfWork : IDisposable
    {

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
