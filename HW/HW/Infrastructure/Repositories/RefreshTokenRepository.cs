using HW.Domain.Entities;
using HW.Domain.Interfaces;

namespace HW.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly Dictionary<string, RefreshToken> _refreshTokens = new();

        public void SaveRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens[refreshToken.Token] = refreshToken;
        }

        public RefreshToken GetRefreshToken(string token)
        {
            _refreshTokens.TryGetValue(token, out var refreshToken);
            return refreshToken;
        }
    }
}
