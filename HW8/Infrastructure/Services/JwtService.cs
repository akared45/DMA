using HW8.Application.DTOs;
using HW8.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HW8.Application.DTOs.AuthDto;

namespace HW8.Infrastructure.Services
{
    public class JwtService : IAuthService
    {
        private const string SecretKey = "kuuga_agito_ryuki_faiz_blade_hibiki_deno_kiva_decade_w_ooo_fourze_wizard_gaim_drive_ghost_exaid_build_zio_zeroone_saber_revice_geats_gotchard_gavv_zeztz";
        private const string Issuer = "AdminApi";
        private const string Audience = "AdminApi";
        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            if (request.Username != "admin" || request.Password != "1")
                throw new UnauthorizedAccessException("Invalid credentials");

            var accessToken = GenerateToken("admin", 1);
            var refreshToken = GenerateToken("admin", 7 * 24 * 60);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        public async Task<TokenResponse> RefreshAsync(RefreshRequest request)
        {
            var principal = ValidateToken(request.RefreshToken);
            if (principal?.Identity?.Name == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var newAccessToken = GenerateToken(principal.Identity.Name, 1);
            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = request.RefreshToken
            };
        }
        private string GenerateToken(string username, int expireMinutes)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private ClaimsPrincipal? ValidateToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var validator = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                return new JwtSecurityTokenHandler().ValidateToken(token, validator, out _);
            }
            catch
            {
                return null;
            }
        }
    }
}
