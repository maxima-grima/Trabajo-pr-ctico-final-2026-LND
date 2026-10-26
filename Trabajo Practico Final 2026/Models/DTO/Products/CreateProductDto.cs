using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0", "99999999", ErrorMessage = "El precio no puede ser negativo.")]
        public decimal Price { get; set; }

        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public int Discount { get; set; }

        public bool HasHappyHour { get; set; }

        public bool Featured { get; set; }

        [Range(1, 100, ErrorMessage = "RecommendedFor debe ser mayor a 0.")]
        public int RecommendedFor { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "La categoría es obligatoria.")]
        public int CategoryId { get; set; }

        // El front lo manda, pero el backend lo ignora y usa el id del token.
        public int? RestaurantId { get; set; }

    }
}
