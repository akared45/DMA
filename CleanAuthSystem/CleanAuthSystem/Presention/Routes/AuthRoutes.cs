using CleanAuthSystem.Application.DTOs;
using CleanAuthSystem.Application.UseCases;
using CleanAuthSystem.Domain.Exceptions;
using CleanAuthSystem.Domain.Repositories;
using CleanAuthSystem.Infrastructure.Repositories;

namespace CleanAuthSystem.Presention.Routes
{
    public static class AuthRoutes
    {
        public static IEndpointRouteBuilder MapAuthRoutes(this IEndpointRouteBuilder app)
        {

            var authGroup = app.MapGroup("/api/auth");
            authGroup.MapPost("/register", async (RegisterRequestDto dto, SendOTPUseCase useCase) =>
            {
                try
                {
                    await useCase.ExecuteAsync(dto);
                    return Results.Ok(new ApiResponseDto(true, "OTP sent, please verify"));
                }
                catch (DomainException ex) 
                { 
                    return Results.BadRequest(new ApiResponseDto(false, ex.Message)); 
                }
                catch (Exception) { 
                    return Results.StatusCode(500); 
                }
            });

            authGroup.MapPost("/verify-otp", async (VerifyOtpRequestDto dto, VerifyOTPUseCase useCase, IOTPRepository otpRepo) =>
            {
                try
                {
                    var memoryRepo = (MemoryOTPRepository)otpRepo;
                    var tempData = await memoryRepo.GetTempDataAsync(dto.Username);
                    if (tempData == null)
                    {
                        return Results.BadRequest(new ApiResponseDto(false, "No registration in progress"));
                    }
                    await useCase.ExecuteAsync(dto, tempData);
                    return Results.Ok(new ApiResponseDto(true, "Registered successfully"));
                }
                catch (DomainException ex) 
                { 
                    return Results.BadRequest(new ApiResponseDto(false, ex.Message)); 
                }
            });
            authGroup.MapPost("/login", async (LoginRequestDto dto, LoginUseCase useCase) =>
            {
                try
                {
                    var user = await useCase.ExecuteAsync(dto);
                    return Results.Ok(new ApiResponseDto(true, "Login successful", "/dashboard"));
                }
                catch (DomainException ex) 
                { 
                    return Results.BadRequest(new ApiResponseDto(false,ex.Message)); 
                }
            });
            return app;
        }
    }
}
