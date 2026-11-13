namespace Trabajo_Practico_Final_2026.Services.Interfaces
{
    public interface IGoogleSheetsService
    {
        Task<Stream> DownloadCsvAsync(string sheetUrl);
    }
}
