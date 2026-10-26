namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class ImportResult
    {
        public int CategoriasCreadas { get; set; }
        public int ProductosCreados { get; set; }
        public int ProductosActualizados { get; set; }
        public List<string> Errores { get; set; } = new();
    }
}
