using CleanAuthSystem.Application.DTOs;
using CleanAuthSystem.Domain.Entities;
using CleanAuthSystem.Domain.Exceptions;
using CleanAuthSystem.Domain.Repositories;

namespace CleanAuthSystem.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepo;

        public LoginUseCase(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<User> ExecuteAsync(LoginRequestDto dto)
        {
            var user = await _userRepo.FindByUsernameAsync(dto.Username);
            if (user == null || user.Password != dto.Password) throw new InvalidCredentialsException();
            if (!user.Verified) throw new UserNotVerifiedException();
            return user;
        }
    }
}
