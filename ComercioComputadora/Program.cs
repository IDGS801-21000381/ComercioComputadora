using Microsoft.EntityFrameworkCore;
using ComercioComputadora.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtener la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

// Agregamos la configuracion para SQLServer
builder.Services.AddDbContext<ComercioComputadoraContext>(options => options.UseSqlServer(connectionString));

// Definimos la nueva política Cross-origin Resource Sharing (CORS o Uso compartido de recursos entre orígenes) para que la API sea accesible para cualquier aplicación.
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activamos la nueva política 
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();