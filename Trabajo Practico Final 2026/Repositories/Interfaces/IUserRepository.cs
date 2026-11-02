using Trabajo_Practico_Final_2026.Entities;

namespace Trabajo_Practico_Final_2026.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int id);
        User? GetByRestaurantName(string restaurantName);
        bool ExistsByRestaurantName(string restaurantName);
        User Create(User user);
        void Update(User user);
        void Delete(User user);
    }
}
