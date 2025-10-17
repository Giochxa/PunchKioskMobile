using PunchKioskMobile.Data.Repositories;
using PunchKioskMobile.Models;

namespace PunchKioskMobile.Services
{
    public class PunchService : IPunchService
    {
        private readonly IPunchRepository _punchRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PunchService(IPunchRepository punchRepository, IEmployeeRepository employeeRepository)
        {
            _punchRepository = punchRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<PunchResult> PunchAsync(string employeeCode, string punchType, string notes = null)
        {
            var employee = await _employeeRepository.GetByCodeAsync(employeeCode);
            if (employee == null)
                return new PunchResult { Success = false, Message = "Employee not found." };

            if (punchType != "IN" && punchType != "OUT")
                return new PunchResult { Success = false, Message = "Invalid punch type. Must be IN or OUT." };

            var now = DateTime.Now;

            if (punchType == "IN" && await _punchRepository.HasPunchedInTodayAsync(employee.Id))
                return new PunchResult { Success = false, Message = "Already punched IN today." };

            if (punchType == "OUT" && await _punchRepository.HasPunchedOutTodayAsync(employee.Id))
                return new PunchResult { Success = false, Message = "Already punched OUT today." };

            if (punchType == "OUT" && !await _punchRepository.HasPunchedInTodayAsync(employee.Id))
                return new PunchResult { Success = false, Message = "Cannot punch OUT without punching IN first." };

            var punch = new Punch
            {
                EmployeeId = employee.Id,
                PunchType = punchType,
                PunchTime = now,
                Notes = notes,
                DeviceInfo = DeviceInfo.Model,
                Location = "Manual Entry"
            };

            var createdPunch = await _punchRepository.AddAsync(punch);

            return new PunchResult
            {
                Success = true,
                Message = $"Successfully punched {punchType} at {now:hh:mm tt}",
                Punch = createdPunch,
                Employee = employee
            };
        }

        public async Task<List<Punch>> GetEmployeePunchesAsync(int employeeId)
        {
            return await _punchRepository.GetByEmployeeIdAsync(employeeId);
        }

        public async Task<List<Punch>> GetTodayPunchesAsync(int employeeId)
        {
            return await _punchRepository.GetTodayPunchesAsync(employeeId);
        }

        public async Task<List<Punch>> GetPunchesByDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _punchRepository.GetPunchesByDateRangeAsync(employeeId, startDate, endDate);
        }

        public async Task<List<Punch>> GetAllPunchesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _punchRepository.GetAllPunchesByDateRangeAsync(startDate, endDate);
        }

        public async Task<bool> HasPunchedInTodayAsync(int employeeId)
        {
            return await _punchRepository.HasPunchedInTodayAsync(employeeId);
        }

        public async Task<bool> HasPunchedOutTodayAsync(int employeeId)
        {
            return await _punchRepository.HasPunchedOutTodayAsync(employeeId);
        }
    }
}