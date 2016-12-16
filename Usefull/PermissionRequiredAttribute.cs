namespace Edi.Advance.Framework.Infrastructure.Security
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Edi.Advance.Core.Service;
    using Edi.Advance.Startup.Infrastructure.IoC;
    
    public class PermissionRequiredAttribute : AuthorizeAttribute
    {
        private readonly IMembershipService membershipService;
        private readonly ISecurityPermissionService permissionService;

        public PermissionRequiredAttribute(params object[] anyOf)
            : this(IoC.Resolve<IMembershipService>(), IoC.Resolve<ISecurityPermissionService>(), anyOf)
        {  }

        public PermissionRequiredAttribute(
            IMembershipService membershipService, 
            ISecurityPermissionService permissionService, 
            params object[] anyOf)
        {
            this.membershipService = membershipService;
            this.permissionService = permissionService;
            AnyOf = anyOf.Cast<Enum>().ToArray();
        }

        protected Enum[] AnyOf { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // TODO MM investigate OutputCaching side effects
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var loggedUser = this.membershipService.GetLoggedUserRole();
            if (loggedUser == null)
            {
                this.HandleUnauthorizedRequest(filterContext);
            }

            if (!this.permissionService.IsAccessibleToUser(loggedUser, AnyOf))
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
