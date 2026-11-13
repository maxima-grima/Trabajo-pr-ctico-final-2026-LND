using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico_Final_2026.Models.DTO.Auth;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginDto credentials)
        {
            string? token = _authenticationService.Authenticate(credentials);

            if (token is null)
                return Unauthorized(new { message = "Nombre de restaurante o contraseña incorrectos." });

            return Ok(new TokenDto { Token = token });
        }
    }
}
