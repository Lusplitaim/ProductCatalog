using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ProductCatalog.Core.Services.Authorization.Requirements
{
    public static class AreaActionRequirements
    {
        public static OperationAuthorizationRequirement CreateRequirement =
            new OperationAuthorizationRequirement() { Name = nameof(CreateRequirement) };

        public static OperationAuthorizationRequirement ReadRequirement =
            new OperationAuthorizationRequirement() { Name = nameof(ReadRequirement) };

        public static OperationAuthorizationRequirement UpdateRequirement =
            new OperationAuthorizationRequirement() { Name = nameof(UpdateRequirement) };

        public static OperationAuthorizationRequirement DeleteRequirement =
            new OperationAuthorizationRequirement() { Name = nameof(DeleteRequirement) };
    }
}
