using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService m_ProductService;
        public ProductsController(IProductService productService)
        {
            m_ProductService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilters filters)
        {
            var result = await m_ProductService.GetAsync(filters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await m_ProductService.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto model)
        {
            var result = await m_ProductService.CreateAsync(model);
            return this.ResolveResult(result, () => Created(nameof(CreateProduct), result.Result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto model)
        {
            var result = await m_ProductService.UpdateAsync(id, model);
            return this.ResolveResult(result, () => Ok(result.Result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await m_ProductService.DeleteAsync(id);
            return this.ResolveResult(result, Ok);
        }
    }
}
