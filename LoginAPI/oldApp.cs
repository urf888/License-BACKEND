/*using Microsoft.EntityFrameworkCore;
using LoginAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurarea conexiunii la baza de date SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Configurarea serviciilor MVC și Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Configurarea CORS pentru a permite cereri de la frontend (localhost:5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:5173") // Permite cereri doar de la acest frontend
                          .AllowAnyMethod()   // Permite toate metodele (GET, POST, PUT, DELETE)
                          .AllowAnyHeader()); // Permite toate headerele
});

var app = builder.Build();

// 🔹 Configurarea middleware-ului Swagger pentru testarea API-ului
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Activează politica CORS înainte de Authorization
app.UseCors("AllowFrontend");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

 app.Run();*/