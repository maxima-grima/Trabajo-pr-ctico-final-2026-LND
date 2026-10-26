using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Practico_Final_2026.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public int RestaurantId { get; set; }
        public decimal Discount { get; set; }
        public bool HasHappyHour { get; set; }
        public bool Featured {get; set; }
        public int RecommendedFor {  get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        public int userId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        public int CategoryId { get; set; }
        


    }
}
