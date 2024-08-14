using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    [Route("api/account/[controller]")]
    public class PermissionsController : BaseController
    {
        private readonly IUserService m_UserService;
        public PermissionsController(IUserService userService)
        {
            m_UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var result = await m_UserService.GetPermissionsAsync();
            return Ok(result);
        }
    }
}
