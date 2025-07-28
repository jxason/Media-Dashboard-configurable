using Microsoft.EntityFrameworkCore;
using FinanzasTaxista_Api.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Agrega logging en consola
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Obtiene la cadena de conexión
var _connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura el DbContext
builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseSqlServer(_connectionStrings)
);

// Agrega servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

try
{
    // Muestra errores detallados en desarrollo
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage(); // Esto muestra errores en el navegador
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{

    Console.WriteLine("? Error al iniciar la aplicación:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}

