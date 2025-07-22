using JWT.Application.Features.CQRS.Handlers.Districthandlers;
using JWT.Application.Interfaces;
using JWT.Application.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Context;
using Persistance.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext ayarı
builder.Services.AddDbContext<JwtContext>(options =>
    options.UseSqlServer("Server=YASINEFEDEMIR\\SQLEXPRESS;Database=JwtProject;Trusted_Connection=True;TrustServerCertificate=True;")
);

// JWT Authentication yapılandırması
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtTokenDefaults.ValidIssuer,
            ValidAudience = JwtTokenDefaults.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
            ClockSkew = TimeSpan.Zero // Token süresi bittiğinde hemen geçersiz say
        };
    });

// CQRS Handler ve Repository bağımlılıkları
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<GetDistrictQueryHandler>();
builder.Services.AddScoped<GetDistrictByIdQueryHandler>();
builder.Services.AddScoped<CreateDistrictCommandHandlers>();
builder.Services.AddScoped<UpdateDistrictCommandHandlers>();
builder.Services.AddScoped<RemoveDistrictCommandHandlers>();


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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
