using Radzen;
using Seller.Components;

var builder = WebApplication.CreateBuilder(args);

// Servicios necesarios
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Servicios de Radzen
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddSingleton<ESeller>();


var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    var connectionString = builder.Configuration.GetConnectionString("ESellerDb");
}
else
{

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Mapear componentes Razor con modos interactivos
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();