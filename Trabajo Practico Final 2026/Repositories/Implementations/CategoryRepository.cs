using Trabajo_Practico_Final_2026.Data;
using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;

namespace Trabajo_Practico_Final_2026.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MenuContext _context;

        public CategoryRepository(MenuContext context)
        {
            _context = context;
        }

        public List<Category> GetByUserId(int userId) =>
            _context.Categories.Where(c => c.UserId == userId).ToList();

        public Category? GetById(int id) => _context.Categories.FirstOrDefault(c => c.Id == id);

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
