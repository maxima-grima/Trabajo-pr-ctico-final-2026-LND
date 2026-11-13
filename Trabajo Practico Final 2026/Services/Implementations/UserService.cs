using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Exceptions;
using Trabajo_Practico_Final_2026.Models.DTO.Users;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAll()
        {
            return _userRepository.GetAll().Select(ToDto).ToList();
        }

        public bool Exists(int id)
        {
            return _userRepository.GetById(id) != null;
        }

        public User? Authenticate(string restaurantName, string password)
        {
            User? user = _userRepository.GetByRestaurantName(restaurantName);

            if (user == null || user.Password != password)
                return null;

            return user;
        }

        public UserDto GetById(int id)
        {
            User user = _userRepository.GetById(id)
                ?? throw new NotFoundException("El restaurante no existe.");
            return ToDto(user);
        }

        public UserDto Create(CreateUserDto dto)
        {
            if (_userRepository.ExistsByRestaurantName(dto.RestaurantName))
                throw new ConflictException("Ya existe un restaurante con ese nombre.");

            User user = new User
            {
                RestaurantName = dto.RestaurantName,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            };

            return ToDto(_userRepository.Create(user));
        }

        public UserDto Update(int id, int loggedUserId, UpdateUserDto dto)
        {
            if (id != loggedUserId)
                throw new ForbiddenException("Solo podés editar tu propia cuenta.");

            User user = _userRepository.GetById(id)
                ?? throw new NotFoundException("El restaurante no existe.");

            User? sameName = _userRepository.GetByRestaurantName(dto.RestaurantName);
            if (sameName != null && sameName.Id != id)
                throw new ConflictException("Ya existe un restaurante con ese nombre.");

            user.RestaurantName = dto.RestaurantName;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Address = dto.Address;
            user.PhoneNumber = dto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = dto.Password;

            _userRepository.Update(user);
            return ToDto(user);
        }

        public void Delete(int id, int loggedUserId)
        {
            if (id != loggedUserId)
                throw new ForbiddenException("Solo podés borrar tu propia cuenta.");

            User user = _userRepository.GetById(id)
                ?? throw new NotFoundException("El restaurante no existe.");

            _userRepository.Delete(user);
        }

        private static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                RestaurantName = user.RestaurantName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
