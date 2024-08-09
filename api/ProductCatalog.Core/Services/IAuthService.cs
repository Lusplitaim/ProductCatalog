using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(CreateUserDto model);
        Task<AuthResult> AuthenticateAsync(SignInUserDto model);
    }
}
