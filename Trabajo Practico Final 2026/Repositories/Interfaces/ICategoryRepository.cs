using Trabajo_Practico_Final_2026.Entities;

namespace Trabajo_Practico_Final_2026.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetByUserId(int userId);
        Category? GetById(int id);
        Category Create(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
