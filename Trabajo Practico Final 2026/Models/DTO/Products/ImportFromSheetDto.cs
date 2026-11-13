using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class ImportFromSheetDto
    {
        [Required(ErrorMessage = "El link de la planilla es obligatorio.")]
        [Url(ErrorMessage = "El link no tiene un formato válido.")]
        public string Url { get; set; } = string.Empty;
    }
}
