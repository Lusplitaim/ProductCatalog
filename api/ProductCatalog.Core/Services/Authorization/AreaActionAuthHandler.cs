using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using ProductCatalog.Core.Models.Enums;
using ProductCatalog.Core.Services.Authorization.Requirements;
using ProductCatalog.Core.Storages;
using ProductCatalog.Core.Utils;

namespace ProductCatalog.Core.Services.Authorization
{
    internal class AreaActionAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, Area>
    {
        private readonly IRolePermissionStorage m_RolePermissionStorage;
        private readonly IAuthUtils m_AuthUtils;
        private readonly Dictionary<OperationAuthorizationRequirement, AreaAction> m_AreaActionMap = new()
        {
            {AreaActionRequirements.CreateRequirement, AreaAction.Create },
            {AreaActionRequirements.ReadRequirement, AreaAction.Read },
            {AreaActionRequirements.UpdateRequirement, AreaAction.Update },
            {AreaActionRequirements.DeleteRequirement, AreaAction.Delete },
        };
        public AreaActionAuthHandler(IRolePermissionStorage permStorage, IAuthUtils authUtils)
        {
            m_RolePermissionStorage = permStorage;
            m_AuthUtils = authUtils;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Area resource)
        {
            var permissions = await m_RolePermissionStorage.GetByUserIdAsync(m_AuthUtils.GetAuthUserId());

            var action = m_AreaActionMap[requirement];

            bool IsActionAllowed = permissions.Any(x => x.Area == resource && x.Action == action);
            if (IsActionAllowed)
            {
                context.Succeed(requirement);
            }

            return;
        }
    }
}
