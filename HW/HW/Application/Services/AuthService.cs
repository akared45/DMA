using HW.Application.DTOs;
using HW.Application.Interfaces;
using HW.Domain.Entities;
using HW.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HW.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            if (request.Username != "Test" || request.Password != "1")
            {
                return null;
            }

            var accessToken = GenerateAccessToken(request.Username);
            var refreshToken = GenerateRefreshToken(request.Username);

            _refreshTokenRepository.SaveRefreshToken(refreshToken);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken)
        {
            var storedToken = _refreshTokenRepository.GetRefreshToken(refreshToken);
            if (storedToken == null || storedToken.ExpiryDate < DateTime.UtcNow)
            {
                return null;
            }

            var newAccessToken = GenerateAccessToken(storedToken.Username);
            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateAccessToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("8Fj@kLz#3nTq!ZxR9pWv%tYbG7sD2uQe");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username)
            }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                Issuer = "CleanAuthSystem",
                Audience = "CleanAuthSystemClient",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string username)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                Username = username
            };
        }
    }
}
