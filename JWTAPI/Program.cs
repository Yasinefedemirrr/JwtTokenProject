using JWT.Application.Features.CQRS.Handlers.CityWeatherHandlers;
using JWT.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------
// ✅ Veritabanı Bağlantısı (SQL Server)
builder.Services.AddDbContext<JwtContext>(options =>
    options.UseSqlServer("Server=YASINEFEDEMIR\\SQLEXPRESS;Database=JwtProject;Trusted_Connection=True;TrustServerCertificate=True;")
);

// ------------------------------------
// ✅ Scoped Servis Kayıtları
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<GetCityWeatherQueryHandler>();
builder.Services.AddScoped<GetCityWeatherByIdQueryHandler>();
builder.Services.AddScoped<CreateCityWeatherCommandHandler>();
builder.Services.AddScoped<UpdateCityWeatherCommandHandler>();
builder.Services.AddScoped<RemoveCityWeatherCommandHandler>();

// ------------------------------------
// ✅ Controller ve Swagger Ayarları
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ------------------------------------
// ✅ Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
