// Services/EmployeeService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PunchKioskMobile.Models;
using PunchKioskMobile.Services.Local;
using System.Linq;

namespace PunchKioskMobile.Services
{
    public class EmployeeService
    {
        private readonly ApiClient _api;
        private readonly LocalDatabaseService _localDb;

        public EmployeeService(ApiClient api, LocalDatabaseService localDb)
        {
            _api = api;
            _localDb = localDb;
        }

        // Fetch from backend and save locally (replace or update)
        public async Task<List<EmployeeDto>> SyncEmployeesAsync()
        {
            // backend should expose /api/employees returning list of EmployeeDto
            var list = await _api.GetAsync<List<EmployeeDto>>("/api/employees");

            if (list != null && list.Count > 0)
            {
                // mark last updated and upsert locally
                foreach (var e in list)
                {
                    await _localDb.UpsertEmployeeAsync(e);
                }
            }

            // return stored local list
            return await _localDb.GetAllEmployeesAsync();
        }

        public Task<List<EmployeeDto>> GetLocalEmployeesAsync() => _localDb.GetAllEmployeesAsync();
    }
}
