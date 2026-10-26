using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Categories
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
    }
}
