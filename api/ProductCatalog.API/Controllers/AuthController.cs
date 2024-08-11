using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService m_AuthService; 
        public AuthController(IAuthService authService)
        {
            m_AuthService = authService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            var result = await m_AuthService.RegisterAsync(model);
            return this.ResolveResult(result, () => Created(nameof(Register), result.Result));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInUserDto model)
        {
            var result = await m_AuthService.AuthenticateAsync(model);
            return this.ResolveResult(result, () => Ok(result.Result));
        }
    }
}
