using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Categories;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto dto)
        {
            CategoryDto category = _categoryService.Create(GetLoggedUserId(), dto);
            return Created($"/api/users/{category.RestaurantId}/categories", category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCategoryDto dto)
        {
            _categoryService.Update(id, GetLoggedUserId(), dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id, GetLoggedUserId());
            return NoContent();
        }
    }
}
