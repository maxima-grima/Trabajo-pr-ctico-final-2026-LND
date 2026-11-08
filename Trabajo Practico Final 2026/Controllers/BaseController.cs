using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Exceptions;

namespace Trabajo_Practico_Final_2026.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int GetLoggedUserId()
        {
            string? value = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (!int.TryParse(value, out int userId))
                throw new UnauthorizedException("Tenés que iniciar sesión.");

            return userId;
        }
    }
}
