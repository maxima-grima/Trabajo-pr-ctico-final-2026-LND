using Microsoft.EntityFrameworkCore;
using Trabajo_Practico_Final_2026.Entities;

namespace Trabajo_Practico_Final_2026.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.RestaurantName)
                .IsUnique();
        }
    }
}