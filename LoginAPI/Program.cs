using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LoginAPI.Data;
using LoginAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurarea conexiunii la baza de date SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Adăugarea serviciilor personalizate
builder.Services.AddScoped<IUserLoginService, UserLoginService>(); // Serviciul pentru login și înregistrare
builder.Services.AddScoped<JwtService>();       // Serviciul pentru generarea token-urilor JWT

// 🔹 Configurarea autentificării cu JWT
var jwtKey = builder.Configuration["JwtSettings:Secret"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Secret Key is missing from configuration.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// 🔹 Configurarea serviciilor MVC și Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Configurarea CORS pentru a permite cereri de la frontend (localhost:5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// 🔹 Configurarea middleware-ului Swagger pentru testarea API-ului
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Activează politica CORS și autentificarea
app.UseCors("AllowFrontend");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
