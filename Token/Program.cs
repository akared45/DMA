using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Token.Data;
using Token.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var secretKey = "v2j8Pj0RJ7N1hR6VJm2uVfLwT2X4KzVtUjzFh8b6+vM=f";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("AppDb"));
builder.Services.AddSingleton<JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "abc",
            ValidAudience = "abc",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new() { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123") },
            new() { Username = "user1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass1") },
            new() { Username = "user2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass2") }
        );
        db.SaveChanges();
    }
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();