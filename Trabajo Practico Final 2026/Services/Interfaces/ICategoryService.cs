using Trabajo_Practico_Final_2026.Models.DTO.Categories;

namespace Trabajo_Practico_Final_2026.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetByRestaurant(int restaurantId);
        bool BelongsToUser(int categoryId, int userId);
        CategoryDto Create(int loggedUserId, CreateCategoryDto dto);
        CategoryDto Update(int id, int loggedUserId, UpdateCategoryDto dto);
        void Delete(int id, int loggedUserId);
    }
}
