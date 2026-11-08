using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;
        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
            {
                _customAuthenticationService = customAuthenticationService;
            }

            [HttpPost("authenticate")]
            public IActionResult Autenticar([FromBody] CredentialsDtoRequest authenticationRequest)
            {
                string? token = _customAuthenticationService.Autenticar(authenticationRequest);

                if (token is null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
        }
    }

