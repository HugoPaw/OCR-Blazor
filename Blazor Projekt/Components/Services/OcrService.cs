using Microsoft.AspNetCore.Components.Forms;

namespace BlazorProjekt.Components.Services
{
    public class OcrService
    {
        public string? FileName { get; private set; }
        public string? FileEnding { get; private set; }
        public byte[]? FileData { get; private set; }

        public void LoadFile(IBrowserFile file)
        {
            FileName = file.Name;
            FileEnding = Path.GetExtension(file.Name);
            using var stream = new MemoryStream();
            file.OpenReadStream().CopyTo(stream);
            FileData = stream.ToArray();
        }

        public string Evaluate()
        {
            // Hier kommt später echte OCR-Logik rein
            return "Noch kein OCR implementiert";
        }
    }
}
