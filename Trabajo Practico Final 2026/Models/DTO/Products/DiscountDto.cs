using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class DiscountDto
    {

        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public int Discount { get; set; }
    }

}