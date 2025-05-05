using BlazorProjekt.Components;
using BlazorProjekt.Services;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + Blazor Server + Fehlerdetails aktivieren
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// Upload-Limit erhöhen
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 15 * 1024 * 1024; // 15 MB
});

// OCR-bezogene Services registrieren
builder.Services.AddScoped<PdfTextExtractor>();
builder.Services.AddScoped<ImageTextExtractor>();
builder.Services.AddScoped<OcrService>();

// HttpClient aktivieren (z. B. für Wetter-API)
builder.Services.AddHttpClient();

var app = builder.Build();

// Fehlerbehandlung und Sicherheitsfeatures
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Razor-Komponenten in Blazor Server aktivieren
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
