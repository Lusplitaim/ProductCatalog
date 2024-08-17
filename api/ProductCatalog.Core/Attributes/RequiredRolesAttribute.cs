using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredRolesAttribute : Attribute
    {
        public IEnumerable<UserRoleType> RequiredRoles { get; }

        public RequiredRolesAttribute(params UserRoleType[] roles)
        {
            RequiredRoles = new HashSet<UserRoleType>(roles);
        }
    }
}
