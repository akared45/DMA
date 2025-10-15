using CleanAuthSystem.Application.DTOs;
using CleanAuthSystem.Domain.Entities;
using CleanAuthSystem.Domain.Exceptions;
using CleanAuthSystem.Domain.Repositories;
using CleanAuthSystem.Domain.Services;

namespace CleanAuthSystem.Application.UseCases
{
    public class SendOTPUseCase
    {
        private readonly IUserRepository _userRepo;
        private readonly IOTPRepository _otpRepo;
        private readonly ISmsService _smsService;

        public SendOTPUseCase(IUserRepository userRepo, IOTPRepository otpRepo, ISmsService smsService)
        {
            _userRepo = userRepo;
            _otpRepo = otpRepo;
            _smsService = smsService;
        }

        public async Task ExecuteAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepo.FindByUsernameAsync(dto.Username);
            if (existingUser != null) throw new UserAlreadyExistsException();

            var otpCode = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(5);
            var otp = new OTP(otpCode, expiry);
            await _otpRepo.SaveOTPAsync(dto.Username, otp);
            await _smsService.SendSmsAsync(dto.Phone, $"Your OTP is {otpCode}");
        }
    }
}
