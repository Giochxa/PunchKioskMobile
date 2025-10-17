using PunchKioskMobile.Models;

namespace PunchKioskMobile.Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<Employee> GetEmployeeByCodeAsync(string employeeCode);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> SearchEmployeesAsync(string searchTerm);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> CanPunchAsync(int employeeId, DateTime punchTime);
        Task<Employee> FindEmployeeAsync(string searchTerm);
    }
}