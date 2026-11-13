using System.Text.RegularExpressions;
using Trabajo_Practico_Final_2026.Exceptions;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Services.Implementations
{
    public class GoogleSheetsService : IGoogleSheetsService
    {
        private readonly HttpClient _httpClient;

        public GoogleSheetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> DownloadCsvAsync(string sheetUrl)
        {
            string exportUrl = BuildExportUrl(sheetUrl);

            HttpResponseMessage response = await _httpClient.GetAsync(exportUrl);
            string? contentType = response.Content.Headers.ContentType?.MediaType;

            if (!response.IsSuccessStatusCode || contentType != "text/csv")
                throw new BadRequestException("No se pudo descargar la planilla. Revisá que esté compartida como \"Cualquier persona con el enlace\".");

            MemoryStream csv = new MemoryStream();
            await response.Content.CopyToAsync(csv);
            csv.Position = 0;
            return csv;
        }

        private static string BuildExportUrl(string sheetUrl)
        {
            Match idMatch = Regex.Match(sheetUrl, @"docs\.google\.com/spreadsheets/d/([a-zA-Z0-9_-]+)");

            if (!idMatch.Success)
                throw new BadRequestException("El link no es de una planilla de Google Sheets.");

            string exportUrl = $"https://docs.google.com/spreadsheets/d/{idMatch.Groups[1].Value}/export?format=csv";

            Match gidMatch = Regex.Match(sheetUrl, @"gid=([0-9]+)");

            if (gidMatch.Success)
                exportUrl += $"&gid={gidMatch.Groups[1].Value}";

            return exportUrl;
        }
    }
}
