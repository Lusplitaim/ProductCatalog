using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ResolveResult(this ControllerBase controller, ExecResult result, Func<IActionResult> getActionResult)
        {
            if (result.Succeeded)
            {
                return getActionResult();
            }

            return controller.BadRequest(new ErrorsModel(result));
        }
    }
}
