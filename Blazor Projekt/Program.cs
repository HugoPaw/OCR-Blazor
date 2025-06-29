using BlazorProjekt.Components;
using BlazorProjekt.Services;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// Upload-Limit erhöhen
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 15 * 1024 * 1024;
});

// Eigene Services
builder.Services.AddScoped<PdfTextExtractor>();
builder.Services.AddScoped<ImageTextExtractor>();
builder.Services.AddScoped<OcrService>();
builder.Services.AddScoped<AiService>();

// HttpClient für Blazor-Komponenten (Frontend)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7115")
});

// Benannter HttpClient für NASA-Zugriffe im Backend (Proxy)
builder.Services.AddHttpClient("NASA", client =>
{
    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
});

// Controller aktivieren
builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
