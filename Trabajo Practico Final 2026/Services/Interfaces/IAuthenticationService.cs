using Trabajo_Practico_Final_2026.Models.DTO.Auth;

namespace Trabajo_Practico_Final_2026.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string? Authenticate(LoginDto credentials);
    }
}
