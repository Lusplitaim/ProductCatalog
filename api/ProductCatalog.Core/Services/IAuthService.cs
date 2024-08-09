using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Services
{
    public interface IAuthService
    {
        Task<ExecResult<AuthResult>> RegisterAsync(CreateUserDto model);
        Task<ExecResult<AuthResult>> AuthenticateAsync(SignInUserDto model);
    }
}
