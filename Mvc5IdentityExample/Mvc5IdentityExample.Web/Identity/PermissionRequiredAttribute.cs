using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mvc5IdentityExample.Domain;
using Mvc5IdentityExample.Domain.Entities;

namespace Mvc5IdentityExample.Web.Identity
{
    public class PermissionRequiredAttribute : AuthorizeAttribute
    {
        private readonly ISecurityPermissionService _securityPermissionService;
        public string[] Permissions { get; set; }

        public PermissionRequiredAttribute(ISecurityPermissionService securityPermissionService, params object[] permissions)
        {
            _securityPermissionService = securityPermissionService;
            Permissions = permissions.Cast<string>().ToArray();
        }

        public PermissionRequiredAttribute(params object [] permissions) : this(IoC.Resolve<ISecurityPermissionService>(), permissions)
        {
            
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userName = httpContext.User.Identity.GetUserName();
            //TODO Get current user and check permissions 
           return _securityPermissionService.HasPermissions(new User(), Permissions);
            //check your permissions
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                //handle no permission
            }
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}