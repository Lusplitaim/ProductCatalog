using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public static UserDto From(UserEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
            };
        }
    }
}
