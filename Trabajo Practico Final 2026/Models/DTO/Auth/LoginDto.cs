using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El nombre del restaurante es obligatorio")]
        public string RestaurantName { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }
}
