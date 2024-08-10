using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.DTOs.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserRole> UserRoles { get; set; } = [];
    }
}
