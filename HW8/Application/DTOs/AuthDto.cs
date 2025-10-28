namespace HW8.Application.DTOs
{
    public class AuthDto
    {
        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class TokenResponse
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
        }

        public class RefreshRequest
        {
            public string RefreshToken { get; set; } = string.Empty;
        }
    }
}
