using HW7.Application.DTOs;
using HW7.Application.Interfaces;
using HW7.Application.Services;
using HW7.Domain.Interfaces;
using HW7.Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.AspNetCore;

namespace HW7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
            builder.Services.AddSingleton<INewsRepository, NewsRepository>();
            builder.Services.AddSingleton<INewsService, NewsService>();
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
            });

            var app = builder.Build();
            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.MapGet("/api/news", async (INewsService newsService) =>
            {
                var news = await newsService.GetAllNewsAsync();
                return Results.Ok(news);
            });

            app.MapGet("/api/news/{id:guid}", async (INewsService newsService, Guid id) =>
            {
                var news = await newsService.GetNewsByIdAsync(id);
                return news != null ? Results.Ok(news) : Results.NotFound();
            });

            app.MapPost("/api/news", async (INewsService newsService, HttpContext context) =>
            {
                var request = await context.Request.ReadFromJsonAsync<CreateNewsRequest>();
                if (request == null) return Results.BadRequest();

                var createdNews = await newsService.CreateNewsAsync(request);
                return Results.Created($"/api/news/{createdNews.Id}", createdNews);
            });

            app.MapPut("/api/news/{id:guid}", async (INewsService newsService, Guid id, HttpContext context) =>
            {
                var request = await context.Request.ReadFromJsonAsync<UpdateNewsRequest>();
                if (request == null) return Results.BadRequest();

                await newsService.UpdateNewsAsync(id, request);
                return Results.NoContent();
            });

            app.MapDelete("/api/news/{id:guid}", async (INewsService newsService, Guid id) =>
            {
                await newsService.DeleteNewsAsync(id);
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
