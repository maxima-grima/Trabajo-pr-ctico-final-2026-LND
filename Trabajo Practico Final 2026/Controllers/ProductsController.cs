using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Products;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_productService.GetById(id));
        }

        [HttpGet("me")]
        public IActionResult GetMine()
        {
            return Ok(_productService.GetMine(GetLoggedUserId()));
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            ProductDto product = _productService.Create(GetLoggedUserId(), dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductDto dto)
        {
            _productService.Update(id, GetLoggedUserId(), dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id, GetLoggedUserId());
            return NoContent();
        }

        [HttpPut("{id}/discount")]
        public IActionResult SetDiscount(int id, DiscountDto dto)
        {
            _productService.SetDiscount(id, GetLoggedUserId(), dto);
            return NoContent();
        }

        [HttpPost("import")]
        public IActionResult Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Tenés que adjuntar un archivo CSV." });

            if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { message = "El archivo tiene que ser .csv." });

            using Stream stream = file.OpenReadStream();
            return Ok(_productService.Import(GetLoggedUserId(), stream));
        }

        [HttpPut("{id}/happy-hour")]
        public IActionResult SetHappyHour(int id, HappyHourDto dto)
        {
            _productService.SetHappyHour(id, GetLoggedUserId(), dto);
            return NoContent();
        }
    }
}
