using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;

namespace BlazorProjekt.Services
{
    public class OcrService
    {
        private readonly PdfTextExtractor _pdfExtractor;
        private readonly ImageTextExtractor _imageExtractor;

        public string? FileName { get; private set; }
        public string? FileEnding { get; private set; }
        public byte[]? FileData { get; private set; }
        public string? FileId { get; private set; }
        public string? OcrMethod { get; private set; }
        public DateTime? EvaluationTime { get; private set; }

        public OcrService(PdfTextExtractor pdfExtractor, ImageTextExtractor imageExtractor)
        {
            _pdfExtractor = pdfExtractor;
            _imageExtractor = imageExtractor;
        }

        public async Task LoadFileAsync(IBrowserFile file)
        {
            FileName = file.Name;
            FileEnding = Path.GetExtension(file.Name).ToLowerInvariant();

            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            FileData = stream.ToArray();

            FileId = GenerateFileId(FileData);
        }

        public string Evaluate()
        {
            EvaluationTime = DateTime.Now;

            if (FileData == null || FileEnding == null)
                return "No file loaded.";

            return FileEnding switch
            {
                ".pdf" => ExtractPdfText(),
                ".png" or ".jpg" or ".jpeg" => ExtractImageText(),
                _ => "Unsupported file format."
            };
        }

        private string ExtractPdfText()
        {
            OcrMethod = "PdfPig";
            Console.WriteLine("ExtractPdfText called");

            try
            {
                return _pdfExtractor.ExtractText(FileData!);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ExtractPdfText: " + ex.Message);
                return $"PDF extraction failed: {ex.Message}";
            }
        }


        private string ExtractImageText()
        {
            OcrMethod = "Tesseract OCR";
            return _imageExtractor.ExtractText(FileData);
        }

        private string GenerateFileId(byte[] data)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
