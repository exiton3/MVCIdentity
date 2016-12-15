using System;

namespace Mvc5IdentityExample.Domain.Entities
{
    public class ExternalLogin
    {
        private UserN _userN;

        #region Scalar Properties
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual Guid UserId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual UserN UserN
        {
            get { return _userN; }
            set
            {
                _userN = value;
                UserId = value.UserId;
            }
        }
        #endregion
    }
}
