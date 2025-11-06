using Microsoft.EntityFrameworkCore;
using UnitOfWork.Application.Services;
using UnitOfWork.Domain.Interfaces;
using UnitOfWork.Infrastructure.Data;
using UnitOfWork.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "server=localhost;database=unitofworkdb;user=root;password=;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 32))) 
);
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkI>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
