using Microsoft.AspNet.Identity;
using Mvc5IdentityExample.Domain;
using Mvc5IdentityExample.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mvc5IdentityExample.Domain.Repositories;

namespace Mvc5IdentityExample.Web.Identity
{
    public class RoleStore : IRoleStore<IdentityRole, Guid>, IQueryableRoleStore<IdentityRole, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        public RoleStore(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        #region IRoleStore<IdentityRole, Guid> Members
        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = GetRole(role);

            _roleRepository.Add(r);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = GetRole(role);

            _roleRepository.Remove(r);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IdentityRole> FindByIdAsync(Guid roleId)
        {
            var role = _roleRepository.FindById(roleId);
            return Task.FromResult<IdentityRole>(GetIdentityRole(role));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var role = _roleRepository.FindByName(roleName);
            return Task.FromResult<IdentityRole>(GetIdentityRole(role));
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            var r = GetRole(role);
            _roleRepository.Update(r);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IQueryableRoleStore<IdentityRole, Guid> Members
        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _roleRepository
                    .GetAll()
                    .Select(x => GetIdentityRole(x))
                    .AsQueryable();
            }
        }
        #endregion

        #region Private Methods
        private Role GetRole(IdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            return new Role
            {
                RoleId = identityRole.Id,
                Name = identityRole.Name
            };
        }

        private IdentityRole GetIdentityRole(Role role)
        {
            if (role == null)
                return null;
            return new IdentityRole
            {
                Id = role.RoleId,
                Name = role.Name
            };
        }
        #endregion
    }
}