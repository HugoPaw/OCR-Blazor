using Tesseract;

namespace BlazorProjekt.Services
{
    public class ImageTextExtractor
    {
        private readonly string _tessdataPath;
        private const string Language = "eng";

        public ImageTextExtractor(IWebHostEnvironment env)
        {
            _tessdataPath = Path.Combine(env.ContentRootPath, "tessdata");
        }

        public string ExtractText(byte[] imageBytes)
        {
            try
            {
                using var engine = new TesseractEngine(_tessdataPath, Language, EngineMode.Default);
                using var img = Pix.LoadFromMemory(imageBytes);
                using var page = engine.Process(img);
                return page.GetText();
            }
            catch (Exception ex)
            {
                return $"Image OCR failed: {ex.Message}";
            }
        }
    }
}
