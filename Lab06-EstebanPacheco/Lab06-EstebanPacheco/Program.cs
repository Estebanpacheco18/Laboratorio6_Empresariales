using System.Text;
using Lab06_EstebanPacheco.Repositories;
using Lab06_EstebanPacheco.Repositories.Interfaces;
using Lab06_EstebanPacheco.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configuración de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MiApp",
            ValidAudience = "MiAppUser",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("claveSuperSegura123456789012345678901234567890")) // Clave secreta más larga
        };
    });

builder.Services.AddAuthorization();

// Registrar IUserService con su implementación UserService
builder.Services.AddScoped<IUserService, UserService>();

// Registrar IUserRepository con su implementación UserRepository
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera el archivo swagger.json
    app.UseSwaggerUI(); // Habilita la interfaz de Swagger
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Middleware de autenticación
app.UseAuthorization();  // Middleware de autorización
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
