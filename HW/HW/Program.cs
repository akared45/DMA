using HW.Application.DTOs;
using HW.Application.Interfaces;
using HW.Application.Services;
using HW.Domain.Interfaces;
using HW.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "CleanAuthSystem",
                    ValidAudience = "CleanAuthSystemClient",
                    IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes("8Fj@kLz#3nTq!ZxR9pWv%tYbG7sD2uQe"))
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapPost("/api/login", async (IAuthService authService, HttpContext context) =>
            {
                var request = await context.Request.ReadFromJsonAsync<LoginRequest>();
                if (request == null)
                {
                    return Results.BadRequest();
                }

                var tokenResponse = await authService.LoginAsync(request);
                return tokenResponse != null ? Results.Ok(tokenResponse) : Results.Unauthorized();
            });

            app.MapPost("/api/refresh", async (IAuthService authService, HttpContext context) =>
            {
                var refreshTokenRequest = await context.Request.ReadFromJsonAsync<RefreshTokenRequest>();
                if (refreshTokenRequest == null)
                {
                    return Results.BadRequest();
                }

                var tokenResponse = await authService.RefreshAccessTokenAsync(refreshTokenRequest.RefreshToken);
                return tokenResponse != null ? Results.Ok(tokenResponse) : Results.Unauthorized();
            });

            app.MapGet("/api/secret", [Authorize] () =>
            {
                return Results.Ok(new
                {
                    Message = "This is secret data!",
                    Data = new
                    {
                        Id = 1,
                        ConfidentialInfo = "Highly sensitive information"
                    }
                });
            });

            app.Run();
        }
    }
}
