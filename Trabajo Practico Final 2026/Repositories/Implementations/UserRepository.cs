using Trabajo_Practico_Final_2026.Data;
using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;

namespace Trabajo_Practico_Final_2026.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MenuContext _context;

        public UserRepository(MenuContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

        public User? GetByRestaurantName(string restaurantName) =>
            _context.Users.FirstOrDefault(u => u.RestaurantName == restaurantName);

        public bool ExistsByRestaurantName(string restaurantName) =>
            _context.Users.Any(u => u.RestaurantName == restaurantName);

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
