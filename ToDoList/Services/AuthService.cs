using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ToDoList.Models.Entities;
using System.Security.Claims;

namespace ToDoList.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:Key"] ?? throw new Exception("A chave Jwt não foi configurada");
            var chave = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescription);
                return tokenHandler.WriteToken(token);
        }
    }
}
