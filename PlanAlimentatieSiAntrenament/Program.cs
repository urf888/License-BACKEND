using Microsoft.EntityFrameworkCore;
using PlanAlimentatieSiAntrenament;
using PlanAlimentatieSiAntrenament.data;

var builder = WebApplication.CreateBuilder(args);

// Adăugăm contextul bazei de date
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adăugăm suport pentru controlere
builder.Services.AddControllers();

// Adăugăm Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurăm middleware pentru Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plan Alimentatie API v1");
        c.RoutePrefix = string.Empty; // Swagger la rădăcina aplicației
    });
}

// Configurăm middleware pentru API
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
