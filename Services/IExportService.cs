using PunchKioskMobile.Models;

namespace PunchKioskMobile.Services
{
    public interface IExportService
    {
        Task<string> ExportToCsvAsync(DateTime startDate, DateTime endDate);
        Task<byte[]> ExportToExcelAsync(DateTime startDate, DateTime endDate);
        Task<string> GeneratePunchReportAsync(DateTime startDate, DateTime endDate);
    }
}