using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
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

        public string? Authenticate(LoginDto credentials)
        {
            User? user = _userService.Authenticate(credentials.RestaurantName, credentials.Password);

            if (user is null)
                return null;

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            SigningCredentials signature = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
                new Claim("restaurantName", user.RestaurantName)
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
