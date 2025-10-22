using FundAdministrationApi.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FundAdministrationApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(LoginRequestDto login)
        {
            // In a real-world scenario, check DB. Here we mock one user.
            if (login.Email == "alkaAdmin@test.com" && login.Password == "Password123")
            {
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, login.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return await Task.FromResult(new AuthResponseDto
                {
                    Token = tokenHandler.WriteToken(token),
                    ExpiresAt = tokenDescriptor.Expires ?? DateTime.UtcNow.AddHours(1)
                });
            }

            return null;
        }
    }
}
