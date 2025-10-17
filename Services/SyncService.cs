// Services/SyncService.cs
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PunchKioskMobile.Services
{
    public class SyncService : IDisposable
    {
        private readonly PunchService _punchService;
        private readonly EmployeeService _employeeService;
        private Timer _timer;

        public SyncService(PunchService punchService, EmployeeService employeeService)
        {
            _punchService = punchService;
            _employeeService = employeeService;
        }

        // Start a periodic sync every N seconds (call Start from app)
        public void StartPeriodicSync(int seconds = 60)
        {
            _timer = new Timer(async _ => await RunSyncAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(seconds));
        }

        public async Task RunSyncAsync()
        {
            try
            {
                // Sync pending punches first
                await _punchService.SyncPendingAsync();

                // Then refresh employee list
                await _employeeService.SyncEmployeesAsync();
            }
            catch
            {
                // swallow errors (optional: add logging)
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
