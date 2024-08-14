using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProductCatalog.Core.Exceptions;

namespace ProductCatalog.Core.Services.Authorization
{
    internal class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;
        private readonly IAuthorizationService m_AuthorizationService;
        public AuthService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            m_HttpContextAccessor = httpContextAccessor;
            m_AuthorizationService = authorizationService;
        }
        public async Task AuthorizeAsync<T>(T requirement, object resource) where T : IAuthorizationRequirement
        {
            var result = await m_AuthorizationService.AuthorizeAsync(m_HttpContextAccessor!.HttpContext!.User, resource, requirement);
            if (!result.Succeeded)
            {
                throw new ForbiddenCoreException();
            }
        }
    }
}
