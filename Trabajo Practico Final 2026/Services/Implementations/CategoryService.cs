using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Exceptions;
using Trabajo_Practico_Final_2026.Models.DTO.Categories;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public List<CategoryDto> GetByRestaurant(int restaurantId)
        {
            if (_userRepository.GetById(restaurantId) == null)
                throw new NotFoundException("El restaurante no existe.");

            return _categoryRepository.GetByUserId(restaurantId).Select(ToDto).ToList();
        }

        public CategoryDto Create(int loggedUserId, CreateCategoryDto dto)
        {
            Category category = new Category
            {
                Name = dto.Name,
                UserId = loggedUserId
            };

            return ToDto(_categoryRepository.Create(category));
        }

        public CategoryDto Update(int id, int loggedUserId, UpdateCategoryDto dto)
        {
            Category category = GetOwnCategory(id, loggedUserId);
            category.Name = dto.Name;
            _categoryRepository.Update(category);
            return ToDto(category);
        }

        public void Delete(int id, int loggedUserId)
        {
            Category category = GetOwnCategory(id, loggedUserId);
            _categoryRepository.Delete(category);
        }

        private Category GetOwnCategory(int id, int loggedUserId)
        {
            Category category = _categoryRepository.GetById(id)
                ?? throw new NotFoundException("La categoría no existe.");

            if (category.UserId != loggedUserId)
                throw new ForbiddenException("La categoría no pertenece a tu restaurante.");

            return category;
        }

        private static CategoryDto ToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                RestaurantId = category.UserId
            };
        }
    }
}
