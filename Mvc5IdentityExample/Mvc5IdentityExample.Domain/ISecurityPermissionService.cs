using System.Collections.Generic;
using Mvc5IdentityExample.Domain.Entities;

namespace Mvc5IdentityExample.Domain
{
    public interface ISecurityPermissionService
    {
        bool HasPermissions(User user, IEnumerable<string> permissions);
    }
}