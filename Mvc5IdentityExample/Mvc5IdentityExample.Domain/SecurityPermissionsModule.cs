using System.Collections.Generic;
using Mvc5IdentityExample.Domain.Entities;

namespace Mvc5IdentityExample.Domain
{
    public abstract class SecurityPermissionsModule : ISecurityPermissionsModule
    {
        readonly List<Permission> _permissions = new List<Permission>();

        protected void AddPermission(string name, string description)
        {
            _permissions.Add(new Permission(name, description));
        }

        protected abstract void Setup();

        public IEnumerable<Permission> GetAllPermisions()
        {
            return _permissions;
        }

    }
}