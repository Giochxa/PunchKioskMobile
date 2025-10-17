using PunchKioskMobile.Models;

namespace PunchKioskMobile.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> GetByCodeAsync(string employeeCode);
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> SearchAsync(string searchTerm);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<bool> EmployeeCodeExistsAsync(string employeeCode);
    }
}