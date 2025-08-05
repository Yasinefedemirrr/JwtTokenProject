using JWT.Application.Features.CQRS.Handlers.CityWeatherHandlers;
using JWT.Application.Features.CQRS.Handlers.Districthandlers;
using JWT.Application.Interfaces;
using JWT.Application.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance.Context;
using Persistance.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JwtContext>(options =>
    options.UseSqlServer("Server=YASINEFEDEMIR\\SQLEXPRESS;Database=JwtProject;Trusted_Connection=True;TrustServerCertificate=True;")
);

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
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<GetCityWeatherQueryHandler>();
builder.Services.AddScoped<GetCityWeatherByIdQueryHandler>();
builder.Services.AddScoped<CreateCityWeatherCommandHandler>();
builder.Services.AddScoped<UpdateCityWeatherCommandHandler>();
builder.Services.AddScoped<RemoveCityWeatherCommandHandler>();

builder.Services.AddScoped<GetDistrictQueryHandler>();
builder.Services.AddScoped<GetDistrictByIdQueryHandler>();
builder.Services.AddScoped<CreateDistrictCommandHandlers>();
builder.Services.AddScoped<UpdateDistrictCommandHandlers>();
builder.Services.AddScoped<RemoveDistrictCommandHandlers>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWT API",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Bearer token giriniz. Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Middleware: Access Token yoksa Refresh Token ile yenile
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["JWTToken"];
    if (string.IsNullOrEmpty(token))
    {
        var refreshToken = context.Request.Cookies["RefreshToken"];
        if (!string.IsNullOrEmpty(refreshToken))
        {
            using var client = new HttpClient();
            var refreshResponse = await client.PostAsync("https://localhost:7270/api/Login/RefreshToken", null);

            if (!refreshResponse.IsSuccessStatusCode)
            {
                // Refresh token da geçersizse login sayfasına yönlendirme yapılabilir
            }
        }
    }

    // Token varsa Authorization header'a ekle
    if (context.Request.Cookies.ContainsKey("JWTToken"))
    {
        var activeToken = context.Request.Cookies["JWTToken"];
        if (!string.IsNullOrEmpty(activeToken))
        {
            context.Request.Headers["Authorization"] = $"Bearer {activeToken}";
        }
    }

    await next();
});

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
