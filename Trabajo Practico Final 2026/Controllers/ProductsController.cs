using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Products;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            return Ok(_productService.GetById(id));
        }

        [HttpGet("me")]
        public ActionResult<List<ProductDto>> GetMine()
        {
            return Ok(_productService.GetMine(GetLoggedUserId()));
        }

        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] CreateProductDto dto)
        {
            ProductDto product = _productService.Create(GetLoggedUserId(), dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDto> Update(int id, [FromBody] UpdateProductDto dto)
        {
            return Ok(_productService.Update(id, GetLoggedUserId(), dto));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id, GetLoggedUserId());
            return NoContent();
        }

        [HttpPut("{id:int}/discount")]
        public ActionResult<ProductDto> SetDiscount(int id, [FromBody] DiscountDto dto)
        {
            return Ok(_productService.SetDiscount(id, GetLoggedUserId(), dto));
        }

        [HttpPut("{id:int}/happyHour")]
        public ActionResult<ProductDto> SetHappyHour(int id, [FromBody] HappyHourDto dto)
        {
            return Ok(_productService.SetHappyHour(id, GetLoggedUserId(), dto));
        }
    }
}
