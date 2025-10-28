using HW8.Application.Interfaces;
using HW8.Domain.Interfaces;
using HW8.Infrastructure.Persistence;
using HW8.Infrastructure.Repositories;
using HW8.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AdminDb"));

builder.Services.AddScoped<IRepository<HW8.Domain.Entities.Article>, ArticleRepository>();
builder.Services.AddScoped<IRepository<HW8.Domain.Entities.Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<HW8.Domain.Entities.Employee>, EmployeeRepository>();

builder.Services.AddScoped<IAuthService, JwtService>();

var key = Encoding.UTF8.GetBytes("kuuga_agito_ryuki_faiz_blade_hibiki_deno_kiva_decade_w_ooo_fourze_wizard_gaim_drive_ghost_exaid_build_zio_zeroone_saber_revice_geats_gotchard_gavv_zeztz");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "AdminApi",
            ValidAudience = "AdminApi",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();