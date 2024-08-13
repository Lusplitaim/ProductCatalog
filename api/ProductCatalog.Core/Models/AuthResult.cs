using ProductCatalog.Core.DTOs.RolePermission;
using ProductCatalog.Core.DTOs.User;

namespace ProductCatalog.Core.Models
{
    public class AuthResult
    {
        public UserDto User { get; set; }
        public ICollection<RolePermissionDto> Permissions { get; set; } = [];
        public string Token { get; set; }
    }
}
