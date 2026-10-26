using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class PriceIncreaseDto
    {
        [Range(typeof(decimal), "0.01", "1000", ErrorMessage = "El porcentaje debe estar entre 0.01 y 1000.")]
        public decimal Percent { get; set; }
    }
}
