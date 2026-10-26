using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Practico_Final_2026.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
         public string Name { get; set; } = string.Empty;

        [ForeignKey ("UserId")]
        public User user { get; set; }
        public int userId { get; set; }
        public ICollection<Product>


    }
}
