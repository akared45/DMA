using static HW8.Application.DTOs.AuthDto;

namespace HW8.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<TokenResponse> RefreshAsync(RefreshRequest request);
    }
}
