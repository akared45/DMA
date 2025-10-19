using HW.Application.DTOs;

namespace HW.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken);
    }
}
