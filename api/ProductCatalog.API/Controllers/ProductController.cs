using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService m_ProductService;
        public ProductController(IProductService productService)
        {
            m_ProductService = productService;
        }

        [HttpGet("editor")]
        public async Task<IActionResult> GetContextForEdit([FromQuery] int? id)
        {
            var result = await m_ProductService.GetContextForEditAsync(id);
            return Ok(result);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetContextForFilters()
        {
            var result = await m_ProductService.GetFiltersContextAsync();
            return Ok(result);
        }
    }
}
