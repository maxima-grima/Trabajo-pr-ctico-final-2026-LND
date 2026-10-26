using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "El nombre del restaurante es obligatorio.")]
        [StringLength(100, MinimumLength = 2)]
        public string RestaurantName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
