using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet("ProductDetail")]
        public async Task<IActionResult> ProductDetailList()
        {
            var values = await _productDetailService.GetAllProductDetailAsync();
            return Ok(values);
        }

        [HttpGet("ProductDetail{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var values = await _productDetailService.GetByIdProductDetailAsync(id);
            return Ok(values);
        }

        [HttpPost("ProductDetail")]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
            return Ok("Məhsul xüsusiyyətləri əlavə olundu");
        }

        [HttpDelete("ProductDetail")]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("Məhsul xüsusiyyətləri silindi");
        }

        [HttpPut("ProductDetail")]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return Ok("Məhsul xüsusiyyətləri güncəlləndi");
        }
    }
}
