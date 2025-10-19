using HW.Domain.Entities;

namespace HW.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void SaveRefreshToken(RefreshToken refreshToken);
        RefreshToken GetRefreshToken(string token);
    }
}
