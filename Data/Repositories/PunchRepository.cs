using Microsoft.EntityFrameworkCore;
using PunchKioskMobile.Models;

namespace PunchKioskMobile.Data.Repositories
{
    public class PunchRepository : IPunchRepository
    {
        private readonly AppDbContext _context;

        public PunchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Punch> GetByIdAsync(int id)
        {
            return await _context.Punches
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Punch>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Punches
                .Where(p => p.EmployeeId == employeeId)
                .OrderByDescending(p => p.PunchTime)
                .ToListAsync();
        }

        public async Task<List<Punch>> GetTodayPunchesAsync(int employeeId)
        {
            var today = DateTime.Today;
            return await _context.Punches
                .Where(p => p.EmployeeId == employeeId &&
                           p.PunchTime.Date == today)
                .OrderBy(p => p.PunchTime)
                .ToListAsync();
        }

        public async Task<List<Punch>> GetPunchesByDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Punches
                .Where(p => p.EmployeeId == employeeId &&
                           p.PunchTime >= startDate &&
                           p.PunchTime <= endDate)
                .OrderBy(p => p.PunchTime)
                .ToListAsync();
        }

        public async Task<List<Punch>> GetAllPunchesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Punches
                .Include(p => p.Employee)
                .Where(p => p.PunchTime >= startDate && p.PunchTime <= endDate)
                .OrderByDescending(p => p.PunchTime)
                .ToListAsync();
        }

        public async Task<Punch> AddAsync(Punch punch)
        {
            punch.CreatedAt = DateTime.UtcNow;
            _context.Punches.Add(punch);
            await _context.SaveChangesAsync();
            return punch;
        }

        public async Task<Punch> UpdateAsync(Punch punch)
        {
            _context.Punches.Update(punch);
            await _context.SaveChangesAsync();
            return punch;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var punch = await GetByIdAsync(id);
            if (punch == null)
                return false;

            _context.Punches.Remove(punch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasPunchedInTodayAsync(int employeeId)
        {
            var today = DateTime.Today;
            return await _context.Punches
                .AnyAsync(p => p.EmployeeId == employeeId &&
                              p.PunchTime.Date == today &&
                              p.PunchType == "IN");
        }

        public async Task<bool> HasPunchedOutTodayAsync(int employeeId)
        {
            var today = DateTime.Today;
            return await _context.Punches
                .AnyAsync(p => p.EmployeeId == employeeId &&
                              p.PunchTime.Date == today &&
                              p.PunchType == "OUT");
        }
    }
}