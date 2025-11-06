using Microsoft.EntityFrameworkCore;
using DBFirst.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversitydbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("UniversityDB"),
        new MariaDbServerVersion(new Version(10, 4, 32))
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
