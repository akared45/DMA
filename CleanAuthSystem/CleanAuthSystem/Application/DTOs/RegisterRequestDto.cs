namespace CleanAuthSystem.Application.DTOs
{
    public record RegisterRequestDto(
        string Username, 
        string Password, 
        string Phone
    );
}
