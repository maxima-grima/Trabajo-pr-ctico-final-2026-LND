using Trabajo_Practico_Final_2026.Models.DTO.Products;

namespace Trabajo_Practico_Final_2026.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetByRestaurant(int restaurantId, int? categoryId, bool? featured, bool? onPromotion);
        List<ProductDto> GetMine(int loggedUserId);
        ProductDto GetById(int id);
        ProductDto Create(int loggedUserId, CreateProductDto dto);
        ProductDto Update(int id, int loggedUserId, UpdateProductDto dto);
        void Delete(int id, int loggedUserId);
        ProductDto SetDiscount(int id, int loggedUserId, DiscountDto dto);
        ProductDto SetHappyHour(int id, int loggedUserId, HappyHourDto dto);
        ImportResult Import(int loggedUserId, Stream csvStream);
    }
}
