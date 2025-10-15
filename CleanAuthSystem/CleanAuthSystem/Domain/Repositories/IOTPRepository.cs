using CleanAuthSystem.Domain.Entities;

namespace CleanAuthSystem.Domain.Repositories
{
    public interface IOTPRepository
    {
        Task SaveOTPAsync(string username, OTP otp);
        Task<OTP?> GetOTPAsync(string username);
        Task DeleteOTPAsync(string username);
    }
}
