using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Filters;

namespace ProductCatalog.API.Controllers
{
    [ApiController]
    [TypeFilter<RestExceptionFilter>]
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
