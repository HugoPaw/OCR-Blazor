using Tesseract;

namespace BlazorProjekt.Services
{
    public class ImageTextExtractor
    {
        private const string Language = "eng"; // kannst du erweitern
        private const string TessdataPath = "wwwroot/tessdata";

        public string ExtractText(byte[] imageBytes)
        {
            try
            {
                using var engine = new TesseractEngine(TessdataPath, Language, EngineMode.Default);
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
