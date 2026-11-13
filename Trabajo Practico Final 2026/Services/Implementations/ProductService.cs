using System.Globalization;
using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Exceptions;
using Trabajo_Practico_Final_2026.Models.DTO.Categories;
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

        public ImportResult Import(int loggedUserId, Stream csvStream)
        {
            ImportResult result = new ImportResult();

            Dictionary<string, int> categories = _categoryService
                .GetByRestaurant(loggedUserId)
                .ToDictionary(c => c.Name.Trim().ToLower(), c => c.Id);

            List<Product> products = _productRepository.GetByUserId(loggedUserId);

            using StreamReader reader = new StreamReader(csvStream);
            reader.ReadLine();

            int lineNumber = 1;
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                List<string> fields = ParseCsvLine(line);

                if (fields.Count < 8)
                {
                    result.Errors.Add($"Fila {lineNumber}: tiene que tener 8 columnas.");
                    continue;
                }

                string categoryName = fields[0];
                string name = fields[1];
                string? description = string.IsNullOrWhiteSpace(fields[2]) ? null : fields[2];

                if (categoryName == string.Empty || name == string.Empty)
                {
                    result.Errors.Add($"Fila {lineNumber}: la categoría y el nombre son obligatorios.");
                    continue;
                }

                if (!TryParsePrice(fields[3], out decimal price) || price < 0)
                {
                    result.Errors.Add($"Fila {lineNumber}: el precio no es válido.");
                    continue;
                }

                if (!int.TryParse(fields[4].TrimEnd('%'), out int discount) || discount < 0 || discount > 100)
                {
                    result.Errors.Add($"Fila {lineNumber}: el descuento debe estar entre 0 y 100.");
                    continue;
                }

                if (!TryParseBool(fields[5], out bool hasHappyHour) || !TryParseBool(fields[6], out bool featured))
                {
                    result.Errors.Add($"Fila {lineNumber}: HappyHour y Destacado deben ser true o false.");
                    continue;
                }

                if (!int.TryParse(fields[7], out int recommendedFor) || recommendedFor < 1)
                {
                    result.Errors.Add($"Fila {lineNumber}: RecomendadoPara debe ser mayor a 0.");
                    continue;
                }

                string categoryKey = categoryName.ToLower();

                if (!categories.TryGetValue(categoryKey, out int categoryId))
                {
                    CategoryDto newCategory = _categoryService.Create(loggedUserId, new CreateCategoryDto { Name = categoryName });
                    categoryId = newCategory.Id;
                    categories[categoryKey] = categoryId;
                    result.CategoriesCreated++;
                }

                Product? existing = products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

                if (existing != null)
                {
                    existing.Description = description;
                    existing.Price = price;
                    existing.Discount = discount;
                    existing.HasHappyHour = hasHappyHour;
                    existing.Featured = featured;
                    existing.RecommendedFor = recommendedFor;
                    existing.CategoryId = categoryId;
                    _productRepository.Update(existing);
                    result.ProductsUpdated++;
                }
                else
                {
                    Product product = new Product
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        Discount = discount,
                        HasHappyHour = hasHappyHour,
                        Featured = featured,
                        RecommendedFor = recommendedFor,
                        CategoryId = categoryId,
                        UserId = loggedUserId
                    };
                    products.Add(_productRepository.Create(product));
                    result.ProductsCreated++;
                }
            }

            return result;
        }

        private static List<string> ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            string current = string.Empty;
            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"' && insideQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current += '"';
                    i++;
                }
                else if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                }
                else if (c == ',' && !insideQuotes)
                {
                    fields.Add(current.Trim());
                    current = string.Empty;
                }
                else
                {
                    current += c;
                }
            }

            fields.Add(current.Trim());
            return fields;
        }

        private static bool TryParsePrice(string value, out decimal price)
        {
            string clean = value.Replace("$", string.Empty).Replace(" ", string.Empty);

            if (clean.Contains(','))
                return decimal.TryParse(clean, NumberStyles.Number, new CultureInfo("es-AR"), out price);

            return decimal.TryParse(clean, NumberStyles.Number, CultureInfo.InvariantCulture, out price);
        }

        private static bool TryParseBool(string value, out bool result)
        {
            string clean = value.Trim().ToLower();
            string[] trueValues = { "true", "verdadero", "si", "sí", "1", "x" };
            string[] falseValues = { "false", "falso", "no", "0", string.Empty };

            result = trueValues.Contains(clean);
            return result || falseValues.Contains(clean);
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
