using Microsoft.AspNetCore.Identity;

namespace ProductCatalog.Core.Data.Entities
{
    public class UserEntity : IdentityUser<int>, ICloneable
    {
        public override string UserName { get; set; }
        public override string Email { get; set; }

        public ICollection<IdentityUserRole<int>> Roles { get; set; } = [];

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
