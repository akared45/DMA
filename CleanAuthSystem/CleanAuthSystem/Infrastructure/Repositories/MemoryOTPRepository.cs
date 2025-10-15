using CleanAuthSystem.Application.DTOs;
using CleanAuthSystem.Domain.Entities;
using CleanAuthSystem.Domain.Repositories;

namespace CleanAuthSystem.Infrastructure.Repositories
{
    public class MemoryOTPRepository : IOTPRepository
    {
        private readonly Dictionary<string, OTP> _store = new();
        private readonly Dictionary<string, RegisterRequestDto> _tempData = new();

        public async Task DeleteOTPAsync(string username)
        {
            _store.Remove(username);
            _tempData.Remove(username);
            await Task.CompletedTask;
        }
        public async Task<OTP?> GetOTPAsync(string username)
        {
            _store.TryGetValue(username, out var otp);
            return await Task.FromResult(otp);
        }
        public async Task SaveOTPAsync(string username, OTP otp)
        {
            _store[username] = otp;
            await Task.CompletedTask;
        }

        public async Task SaveTempDataAsync(string username, RegisterRequestDto data)
        {
            _tempData[username] = data;
            await Task.CompletedTask;
        }

        public async Task<RegisterRequestDto?> GetTempDataAsync(string username)
        {
            _tempData.TryGetValue(username, out var data);
            return await Task.FromResult(data);
        }
    }

}
