﻿using System;

namespace Mvc5IdentityExample.Domain.Entities
{
    public class Claim
    {
        private UserN _userN;

        #region Scalar Properties
        public virtual int ClaimId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
        #endregion

        #region Navigation Properties
        public virtual UserN UserN
        {
            get { return _userN; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _userN = value;
                UserId = value.UserId;
            }
        }
        #endregion
    }
}
