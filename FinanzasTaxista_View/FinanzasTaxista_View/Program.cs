using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------
// Configuraci�n del registro (depuraci�n)
// -------------------------------------------
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// -------------------------------------------
// Registro de servicios
// -------------------------------------------
builder.Services.AddRazorPages();

// HttpClient servicios
builder.Services.AddHttpClient<UsuarioService>();
builder.Services.AddHttpClient<RolService>();
builder.Services.AddHttpClient<DiaTrabajoService>();
builder.Services.AddHttpClient<CategoriaService>();
builder.Services.AddHttpClient<ViajeService>();

// -------------------------------------------
// Autenticaci�n y autorizaci�n
// -------------------------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/IniciarSesion"; // Ruta para inicio de sesi�n.
        options.AccessDeniedPath = "/Account/IniciarSesion";  // Ruta para acceso denegado.
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Administrador"));

    options.AddPolicy("UserPolicy", policy =>
        policy.RequireRole("Taxista"));
});

// -------------------------------------------
//  Construir aplicaci�n
// -------------------------------------------
var app = builder.Build();

// -------------------------------------------
// Configuraci�n del canal de peticiones HTTP
// -------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Account/IniciarSesion");
    return Task.CompletedTask;
});


app.Run();
