using PunchKioskMobile.Models;

namespace PunchKioskMobile.Data.Repositories
{
    public interface IPunchRepository
    {
        Task<Punch> GetByIdAsync(int id);
        Task<List<Punch>> GetByEmployeeIdAsync(int employeeId);
        Task<List<Punch>> GetTodayPunchesAsync(int employeeId);
        Task<List<Punch>> GetPunchesByDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<List<Punch>> GetAllPunchesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Punch> AddAsync(Punch punch);
        Task<Punch> UpdateAsync(Punch punch);
        Task<bool> DeleteAsync(int id);
        Task<bool> HasPunchedInTodayAsync(int employeeId);
        Task<bool> HasPunchedOutTodayAsync(int employeeId);
    }
}