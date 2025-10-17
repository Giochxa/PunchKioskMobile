using PunchKioskMobile.Models;

namespace PunchKioskMobile.Services
{
    public class ExportService : IExportService
    {
        private readonly IPunchService _punchService;

        public ExportService(IPunchService punchService)
        {
            _punchService = punchService;
        }

        public async Task<string> ExportToCsvAsync(DateTime startDate, DateTime endDate)
        {
            var punches = await _punchService.GetAllPunchesByDateRangeAsync(startDate, endDate);

            var csv = new System.Text.StringBuilder();
            csv.AppendLine("EmployeeCode,FullName,Position,PunchType,PunchTime,Notes");

            foreach (var punch in punches)
            {
                csv.AppendLine($"\"{punch.Employee.EmployeeCode}\",\"{punch.Employee.FullName}\",\"{punch.Employee.Position}\",\"{punch.PunchType}\",\"{punch.PunchTime:yyyy-MM-dd HH:mm:ss}\",\"{punch.Notes}\"");
            }

            return csv.ToString();
        }

        public async Task<byte[]> ExportToExcelAsync(DateTime startDate, DateTime endDate)
        {
            var csvContent = await ExportToCsvAsync(startDate, endDate);
            return System.Text.Encoding.UTF8.GetBytes(csvContent);
        }

        public async Task<string> GeneratePunchReportAsync(DateTime startDate, DateTime endDate)
        {
            var punches = await _punchService.GetAllPunchesByDateRangeAsync(startDate, endDate);

            var report = new System.Text.StringBuilder();
            report.AppendLine($"Punch Report for {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
            report.AppendLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine();
            report.AppendLine($"Total Punches: {punches.Count}");
            report.AppendLine();

            var groupedByEmployee = punches.GroupBy(p => p.Employee);

            foreach (var group in groupedByEmployee)
            {
                report.AppendLine($"Employee: {group.Key.FullName} ({group.Key.EmployeeCode})");
                report.AppendLine($"Position: {group.Key.Position}");
                report.AppendLine("Punches:");

                foreach (var punch in group.OrderBy(p => p.PunchTime))
                {
                    report.AppendLine($"  {punch.PunchTime:yyyy-MM-dd HH:mm:ss} - {punch.PunchType} {punch.Notes}");
                }
                report.AppendLine();
            }

            return report.ToString();
        }
    }
}