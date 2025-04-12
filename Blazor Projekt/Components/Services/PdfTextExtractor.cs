using System.Text;
using UglyToad.PdfPig;

namespace BlazorProjekt.Services
{
    public class PdfTextExtractor
    {
        public string ExtractText(byte[] pdfBytes)
        {
            try
            {
                using var stream = new MemoryStream(pdfBytes);
                using var document = PdfDocument.Open(stream);
                var text = new StringBuilder();

                foreach (var page in document.GetPages())
                {
                    text.AppendLine(page.Text);
                }

                return text.ToString();
            }
            catch (Exception ex)
            {
                return $"PDF extraction failed: {ex.Message}";
            }
        }
    }
}
