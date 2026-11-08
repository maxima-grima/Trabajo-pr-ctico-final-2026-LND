using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Users;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            UserDto user = _userService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDto dto)
        {
            _userService.Update(id, GetLoggedUserId(), dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id, GetLoggedUserId());
            return NoContent();
        }

        [HttpGet("{id}/categories")]
        public IActionResult GetCategories(int id)
        {
            return Ok(_categoryService.GetByRestaurant(id));
        }

        [HttpGet("{id}/products")]
        public IActionResult GetProducts(int id, int? categoryId, bool? featured, bool? onPromotion)
        {
            return Ok(_productService.GetByRestaurant(id, categoryId, featured, onPromotion));
        }
    }
}
