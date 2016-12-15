using System.Threading;
using System.Threading.Tasks;
using Mvc5IdentityExample.Domain;
using Mvc5IdentityExample.Domain.Repositories;

namespace Mvc5IdentityExample.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructors

        public UnitOfWork(string nameOrConnectionString)
        {
            Context = new ApplicationDbContext(nameOrConnectionString);
        }

        #endregion

        internal ApplicationDbContext Context { get; }


        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _externalLoginRepository = null;
            _roleRepository = null;
            _userNRepository = null;
            Context.Dispose();
        }

        #endregion

        #region Fields

        private IExternalLoginRepository _externalLoginRepository;
        private IRoleRepository _roleRepository;
        private IUserNRepository _userNRepository;

        #endregion
    }
}