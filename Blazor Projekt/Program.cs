using BlazorProjekt.Components;
using BlazorProjekt.Services;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + Blazor Server + Fehlerdetails aktivieren
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// Upload-Limit erhöhen (z. B. 15 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 15 * 1024 * 1024; // 15 MB
});

// OCR-bezogene Services registrieren
builder.Services.AddScoped<PdfTextExtractor>();
builder.Services.AddScoped<ImageTextExtractor>();
builder.Services.AddScoped<OcrService>();

// AI-Service registrieren
builder.Services.AddScoped<AiService>();

// HttpClient mit BaseAddress für Uploads etc.
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7115") // Passe ggf. den Port an
});

// API-Controller aktivieren (z. B. UploadController)
builder.Services.AddControllers();

var app = builder.Build();

// Fehlerbehandlung und Sicherheitsfeatures
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // nötig für Controller

app.UseAntiforgery();

// API-Routen aktivieren
app.MapControllers();

// Blazor-Komponenten aktivieren
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
