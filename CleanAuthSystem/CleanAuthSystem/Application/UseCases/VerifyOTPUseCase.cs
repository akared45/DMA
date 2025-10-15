using CleanAuthSystem.Application.DTOs;
using CleanAuthSystem.Domain.Entities;
using CleanAuthSystem.Domain.Exceptions;
using CleanAuthSystem.Domain.Repositories;

namespace CleanAuthSystem.Application.UseCases
{
    public class VerifyOTPUseCase
    {
        private readonly IUserRepository _userRepo;
        private readonly IOTPRepository _otpRepo;

        public VerifyOTPUseCase(IUserRepository userRepo, IOTPRepository otpRepo)
        {
            _userRepo = userRepo;
            _otpRepo = otpRepo;
        }
        public async Task<User> ExecuteAsync(VerifyOtpRequestDto dto, RegisterRequestDto tempData)
        {
            var storedOtp = await _otpRepo.GetOTPAsync(dto.Username);
            if (storedOtp == null || storedOtp.Expiry < DateTime.UtcNow || storedOtp.Code != dto.Otp)
                throw new OTPExpiredException();

            var newUser = new User(tempData.Username, tempData.Password, tempData.Phone, true);
            await _userRepo.SaveAsync(newUser);
            await _otpRepo.DeleteOTPAsync(dto.Username);
            return newUser;
        }
    }
}
