using System.Collections.Generic;

namespace Mvc5IdentityExample.Domain.Entities
{
    public class UserGroup
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}