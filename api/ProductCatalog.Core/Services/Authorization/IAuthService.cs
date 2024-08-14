using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Core.Services.Authorization
{
    public interface IAuthService
    {
        Task AuthorizeAsync<T>(T requirement, object resource) where T: IAuthorizationRequirement;
    }
}
