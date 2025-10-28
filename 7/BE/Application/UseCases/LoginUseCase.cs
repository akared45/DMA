using HW7.Domain.Interfaces;

namespace HW7.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IAuthService _authService;

        public LoginUseCase(IAuthService authService)
        {
            _authService = authService;
        }
        public string Execute(string username, string password)
        {
            if (_authService.ValidateCredentials(username, password))
            {
                return _authService.GenerateToken(username);
            }
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}
