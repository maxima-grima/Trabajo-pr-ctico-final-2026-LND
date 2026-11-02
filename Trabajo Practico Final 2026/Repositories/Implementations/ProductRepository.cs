using Trabajo_Practico_Final_2026.Data;
using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;

namespace Trabajo_Practico_Final_2026.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly MenuContext _context;

        public ProductRepository(MenuContext context)
        {
            _context = context;
        }

        public List<Product> GetByUserId(int userId, int? categoryId = null, bool? featured = null, bool? onPromotion = null)
        {
            IQueryable<Product> query = _context.Products.Where(p => p.UserId == userId);

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (featured.HasValue)
                query = query.Where(p => p.Featured == featured.Value);

            if (onPromotion == true)
                query = query.Where(p => p.Discount > 0 || p.HasHappyHour);

            return query.ToList();
        }

        public Product? GetById(int id) => _context.Products.FirstOrDefault(p => p.Id == id);

        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
