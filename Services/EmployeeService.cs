using PunchKioskMobile.Data.Repositories;
using PunchKioskMobile.Models;

namespace PunchKioskMobile.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPunchRepository _punchRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IPunchRepository punchRepository)
        {
            _employeeRepository = employeeRepository;
            _punchRepository = punchRepository;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            return await _employeeRepository.GetByCodeAsync(employeeCode);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<List<Employee>> SearchEmployeesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllEmployeesAsync();

            return await _employeeRepository.SearchAsync(searchTerm);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            if (await _employeeRepository.EmployeeCodeExistsAsync(employee.EmployeeCode))
                throw new InvalidOperationException($"Employee with code {employee.EmployeeCode} already exists.");

            return await _employeeRepository.AddAsync(employee);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var existing = await _employeeRepository.GetByIdAsync(employee.Id);
            if (existing == null)
                throw new ArgumentException("Employee not found.");

            if (existing.EmployeeCode != employee.EmployeeCode &&
                await _employeeRepository.EmployeeCodeExistsAsync(employee.EmployeeCode))
                throw new InvalidOperationException($"Employee with code {employee.EmployeeCode} already exists.");

            return await _employeeRepository.UpdateAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<bool> CanPunchAsync(int employeeId, DateTime punchTime)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return false;

            return true;
        }

        public async Task<Employee> FindEmployeeAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return null;

            var employees = await _employeeRepository.SearchAsync(searchTerm);
            return employees.FirstOrDefault();
        }
    }
}