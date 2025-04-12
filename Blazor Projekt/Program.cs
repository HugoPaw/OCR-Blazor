using BlazorProjekt.Components;
using BlazorProjekt.Services; // <-- aktualisierter Namespace
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Allow larger uploads (e.g. PDFs up to 15 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 15 * 1024 * 1024; // 15 MB
});

// Register OCR-related services (modular)
builder.Services.AddScoped<PdfTextExtractor>();
builder.Services.AddScoped<ImageTextExtractor>();
builder.Services.AddScoped<OcrService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
