using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Categories;
using Trabajo_Practico_Final_2026.Models.DTO.Products;
using Trabajo_Practico_Final_2026.Models.DTO.Users;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public UsersController(IUserService userService, ICategoryService categoryService, IProductService productService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost]
        public ActionResult<UserDto> Create([FromBody] CreateUserDto dto)
        {
            UserDto user = _userService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserDto> Update(int id, [FromBody] UpdateUserDto dto)
        {
            return Ok(_userService.Update(id, GetLoggedUserId(), dto));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id, GetLoggedUserId());
            return NoContent();
        }

        [HttpGet("{id:int}/categories")]
        public ActionResult<List<CategoryDto>> GetCategories(int id)
        {
            return Ok(_categoryService.GetByRestaurant(id));
        }

        [HttpGet("{id:int}/products")]
        public ActionResult<List<ProductDto>> GetProducts(int id, [FromQuery] int? categoryId, [FromQuery] bool? featured, [FromQuery] bool? onPromotion)
        {
            return Ok(_productService.GetByRestaurant(id, categoryId, featured, onPromotion));
        }
    }
}
