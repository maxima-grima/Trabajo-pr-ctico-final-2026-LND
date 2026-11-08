using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Categories;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public ActionResult<CategoryDto> Create([FromBody] CreateCategoryDto dto)
        {
            CategoryDto category = _categoryService.Create(GetLoggedUserId(), dto);
            return StatusCode(StatusCodes.Status201Created, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryDto> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            return Ok(_categoryService.Update(id, GetLoggedUserId(), dto));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id, GetLoggedUserId());
            return NoContent();
        }
    }
}
