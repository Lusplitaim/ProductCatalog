using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAsync();
        Task<ExecResult<UserDto>> CreateAsync(CreateUserDto model);
        Task<ExecResult<UserDto>> UpdateAsync(int userId, UpdateUserDto model);
        Task<ExecResult> DeleteAsync(int userId);
    }
}