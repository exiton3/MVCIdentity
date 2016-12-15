using System;
using System.Collections.Generic;

namespace Mvc5IdentityExample.Domain.Entities
{
    public class Role
    {
        #region Fields
        private ICollection<UserN> _users;
        #endregion

        #region Scalar Properties
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<UserN> Users
        {
            get { return _users ?? (_users = new List<UserN>()); }
            set { _users = value; }
        }
        #endregion
    }
}
