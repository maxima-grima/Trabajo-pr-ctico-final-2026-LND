using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trabajo_Practico_Final_2026.Entities;
using Trabajo_Practico_Final_2026.Models.DTO.Auth;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthenticationService(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public string? Autenticar(LoginDto credentials)

        {
            // proceso de verificación de las credenciales del usuario, si las mismas son válidas entonces el método del servicio devuelve un objeto Usuario el 
            //cual vamos a usar para armar el payload del token.
            User? user = _userService.Authenticate(
                credentials.RestaurantName,
                credentials.Password);

            if (user is null)
            {
                return null; //retornamos null para que el controller devuelva un Unauthorized();
            }

            //proceso de generación del JWT
            var secretPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var signature = new SigningCredentials(
                secretPassword,
                SecurityAlgorithms.HmacSha256); // dejamos casi lista la signature con el secreto y el algoritmo de hashing indicado

            // Agregamos claims a una lista la cual va a ser añadida al payload a las claims por defecto.
            // un claim es cada pieza de información que viaja dentro del token. En este caso referidas al usuario.
            var claimsForToken = new List<Claim>
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("restaurantName", user.RestaurantName)
        };

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }
    }
}
}
