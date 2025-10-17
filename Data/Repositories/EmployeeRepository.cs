using Microsoft.EntityFrameworkCore;
using PunchKioskMobile.Models;

namespace PunchKioskMobile.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }

        public async Task<List<Employee>> SearchAsync(string searchTerm)
        {
            return await _context.Employees
                .Where(e => e.FullName.Contains(searchTerm) ||
                           e.EmployeeCode.Contains(searchTerm) ||
                           e.Position.Contains(searchTerm))
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            employee.CreatedAt = DateTime.UtcNow;
            employee.UpdatedAt = DateTime.UtcNow;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            employee.UpdatedAt = DateTime.UtcNow;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmployeeCodeExistsAsync(string employeeCode)
        {
            return await _context.Employees
                .AnyAsync(e => e.EmployeeCode == employeeCode);
        }
    }
}