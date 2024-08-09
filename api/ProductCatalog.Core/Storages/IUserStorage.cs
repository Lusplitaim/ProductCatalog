using ProductCatalog.Core.DTOs.User;

namespace ProductCatalog.Core.Storages
{
    public interface IUserStorage
    {
        Task<bool> CreateAsync(CreateUserDto model);
        Task<UserDto> GetAsync(string email);
    }
}
