namespace Mvc5IdentityExample.Domain
{
    class UserManagementSecurityPermissionsModule : SecurityPermissionsModule
    {
        protected override void Setup()
        {
            AddPermission(Permmisions.EditUser,"Edit user permission");
            AddPermission(Permmisions.CreateUser,"Create user permission");
            AddPermission(Permmisions.ViewUser,"View user permission");
        }
    }
}