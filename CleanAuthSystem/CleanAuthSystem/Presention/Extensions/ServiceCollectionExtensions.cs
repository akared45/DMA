using CleanAuthSystem.Application.UseCases;
using CleanAuthSystem.Domain.Repositories;
using CleanAuthSystem.Domain.Services;
using CleanAuthSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var twilioConfig = configuration.GetSection("Twilio");

        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");
        services.AddSingleton<IUserRepository>(new FileUserRepository(filePath));
        services.AddSingleton<IOTPRepository>(new MemoryOTPRepository());

        var accountSid = twilioConfig["AccountSid"] ?? throw new InvalidOperationException("Twilio:AccountSid is not configured.");
        var authToken = twilioConfig["AuthToken"] ?? throw new InvalidOperationException("Twilio:AuthToken is not configured.");
        var phoneNumber = twilioConfig["PhoneNumber"] ?? throw new InvalidOperationException("Twilio:PhoneNumber is not configured.");

        services.AddSingleton<ISmsService>(new TwilioSmsService(
            accountSid,
            authToken,
            phoneNumber));
        services.AddScoped<SendOTPUseCase>();
        services.AddScoped<VerifyOTPUseCase>();
        services.AddScoped<LoginUseCase>();

        services.AddCors(options =>
            options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}