using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Exceptions;
using Trabajo_Practico_Final_2026.Models.DTO.Products;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService, IUserService userService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _userService = userService;
        }

        public List<ProductDto> GetByRestaurant(int restaurantId, int? categoryId, bool? featured, bool? onPromotion)
        {
            if (!_userService.Exists(restaurantId))
                throw new NotFoundException("El restaurante no existe.");

            return _productRepository
                .GetByUserId(restaurantId, categoryId, featured, onPromotion)
                .Select(ToDto)
                .ToList();
        }

        public List<ProductDto> GetMine(int loggedUserId)
        {
            return _productRepository.GetByUserId(loggedUserId).Select(ToDto).ToList();
        }

        public ProductDto GetById(int id)
        {
            Product product = _productRepository.GetById(id)
                ?? throw new NotFoundException("El producto no existe.");
            return ToDto(product);
        }

        public ProductDto Create(int loggedUserId, CreateProductDto dto)
        {
            ValidateCategory(dto.CategoryId, loggedUserId);

            Product product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Discount = dto.Discount,
                HasHappyHour = dto.HasHappyHour,
                Featured = dto.Featured,
                RecommendedFor = dto.RecommendedFor,
                CategoryId = dto.CategoryId,
                UserId = loggedUserId
            };

            return ToDto(_productRepository.Create(product));
        }

        public ProductDto Update(int id, int loggedUserId, UpdateProductDto dto)
        {
            Product product = GetOwnProduct(id, loggedUserId);
            ValidateCategory(dto.CategoryId, loggedUserId);

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Discount = dto.Discount;
            product.HasHappyHour = dto.HasHappyHour;
            product.Featured = dto.Featured;
            product.RecommendedFor = dto.RecommendedFor;
            product.CategoryId = dto.CategoryId;

            _productRepository.Update(product);
            return ToDto(product);
        }

        public void Delete(int id, int loggedUserId)
        {
            Product product = GetOwnProduct(id, loggedUserId);
            _productRepository.Delete(product);
        }

        public ProductDto SetDiscount(int id, int loggedUserId, DiscountDto dto)
        {
            Product product = GetOwnProduct(id, loggedUserId);
            product.Discount = dto.Discount;
            _productRepository.Update(product);
            return ToDto(product);
        }

        public ProductDto SetHappyHour(int id, int loggedUserId, HappyHourDto dto)
        {
            Product product = GetOwnProduct(id, loggedUserId);
            product.HasHappyHour = dto.HasHappyHour;
            _productRepository.Update(product);
            return ToDto(product);
        }

        private Product GetOwnProduct(int id, int loggedUserId)
        {
            Product product = _productRepository.GetById(id)
                ?? throw new NotFoundException("El producto no existe.");

            if (product.UserId != loggedUserId)
                throw new ForbiddenException("El producto no pertenece a tu restaurante.");

            return product;
        }

        private void ValidateCategory(int categoryId, int loggedUserId)
        {
            if (!_categoryService.BelongsToUser(categoryId, loggedUserId))
                throw new BadRequestException("La categoría no existe o no pertenece a tu restaurante.");
        }

        private static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                HasHappyHour = product.HasHappyHour,
                Featured = product.Featured,
                RecommendedFor = product.RecommendedFor,
                CategoryId = product.CategoryId,
                RestaurantId = product.UserId
            };
        }
    }
}
