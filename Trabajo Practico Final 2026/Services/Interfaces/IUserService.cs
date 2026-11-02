using Trabajo_Practico_Final_2026.Models.DTO.Users;

namespace Trabajo_Practico_Final_2026.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        UserDto Create(CreateUserDto dto);
        UserDto Update(int id, int loggedUserId, UpdateUserDto dto);
        void Delete(int id, int loggedUserId);
    }
}
