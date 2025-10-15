namespace CleanAuthSystem.Application.DTOs
{
    public record ApiResponseDto(
            bool Success,
            string Message,
            string? Redirect = null
        );
}
