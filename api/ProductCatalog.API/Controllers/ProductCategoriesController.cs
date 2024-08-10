using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class ProductCategoriesController : BaseController
    {
        private readonly IProductCategoryService m_ProductCategoryService;
        public ProductCategoriesController(IProductCategoryService pcs)
        {
            m_ProductCategoryService = pcs;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await m_ProductCategoryService.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
        {
            var result = await m_ProductCategoryService.CreateAsync(model);
            return this.ResolveResult(result, () => Created(nameof(CreateCategory), result.Result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateProductCategoryDto model)
        {
            var result = await m_ProductCategoryService.UpdateAsync(id, model);
            return this.ResolveResult(result, () => Ok(result.Result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await m_ProductCategoryService.DeleteAsync(id);
            return this.ResolveResult(result, Ok);
        }
    }
}
