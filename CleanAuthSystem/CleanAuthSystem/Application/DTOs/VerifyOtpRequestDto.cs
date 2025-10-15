namespace CleanAuthSystem.Application.DTOs
{
    public record VerifyOtpRequestDto
    (
        string Username,
        string Otp
    );
}
