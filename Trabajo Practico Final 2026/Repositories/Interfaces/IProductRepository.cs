using Trabajo_Practico_Final_2026.Entities;

namespace Trabajo_Practico_Final_2026.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetByUserId(int userId, int? categoryId = null, bool? featured = null, bool? onPromotion = null);
        Product? GetById(int id);
        Product Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
