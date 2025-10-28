using HW7.Domain.Entities;
using HW7.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HW7.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _secretKey = "Kuuga_Agito_Ryuki_Faiz_Blade_Hibiki_Kabuto_DenO_Kiva_Decade_W_OOO_Fourze_Wizard_Gaim_Drive_Ghost_ExAid_Build_ZiO_ZeroOne_Saber_Revice_Geats_Gavv_Zeztz";
        private readonly User _user = new User
        {
            Username = "admin",
            Password = "123456"
        };
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateCredentials(string username, string password)
        {
            return username == _user.Username && password == _user.Password;
        }
    }
}
