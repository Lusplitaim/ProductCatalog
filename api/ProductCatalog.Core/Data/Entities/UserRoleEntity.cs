using Microsoft.AspNetCore.Identity;

namespace ProductCatalog.Core.Data.Entities
{
    public class UserRoleEntity : IdentityRole<int>
    {
        public override string Name { get; set; }
    }
}
