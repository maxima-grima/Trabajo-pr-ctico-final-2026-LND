namespace Trabajo_Practico_Final_2026.Models.DTO.Products
{
    public class ImportResult
    {
        public int CategoriesCreated { get; set; }
        public int ProductsCreated { get; set; }
        public int ProductsUpdated { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
