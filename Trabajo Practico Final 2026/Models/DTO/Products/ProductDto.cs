namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public bool HasHappyHour { get; set; }
        public bool Featured { get; set; }
        public int RecommendedFor { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
    }
}
