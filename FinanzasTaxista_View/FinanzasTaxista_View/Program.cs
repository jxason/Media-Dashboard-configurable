

using FinanzasTaxista_View.Service;
var builder = WebApplication.CreateBuilder(args);


//debuggin config
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<UsuarioService>();
builder.Services.AddHttpClient<RolService>();
builder.Services.AddHttpClient<DiaTrabajoService>();
builder.Services.AddHttpClient<CategoriaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
