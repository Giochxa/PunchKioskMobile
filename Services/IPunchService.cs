using PunchKioskMobile.Models;

namespace PunchKioskMobile.Services
{
    public interface IPunchService
    {
        Task<PunchResult> PunchAsync(string employeeCode, string punchType, string notes = null);
        Task<List<Punch>> GetEmployeePunchesAsync(int employeeId);
        Task<List<Punch>> GetTodayPunchesAsync(int employeeId);
        Task<List<Punch>> GetPunchesByDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<List<Punch>> GetAllPunchesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> HasPunchedInTodayAsync(int employeeId);
        Task<bool> HasPunchedOutTodayAsync(int employeeId);
    }

    public class PunchResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Punch Punch { get; set; }
        public Employee Employee { get; set; }
    }
}