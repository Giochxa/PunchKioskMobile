// Services/PunchService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PunchKioskMobile.Models;
using PunchKioskMobile.Services.Local;

namespace PunchKioskMobile.Services
{
    public class PunchService
    {
        private readonly ApiClient _api;
        private readonly LocalDatabaseService _localDb;
        private readonly SyncService _syncService;

        public PunchService(ApiClient api, LocalDatabaseService localDb, SyncService syncService)
        {
            _api = api;
            _localDb = localDb;
            _syncService = syncService;
        }

        // Submit a punch: try online, fallback to local pending queue
        public async Task<bool> SubmitPunchAsync(PunchDto punch)
        {
            try
            {
                // Attempt immediate send to backend endpoint /api/punch
                var response = await _api.PostAsync("/api/punch", punch);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                // ignore and fall back to local storage
            }

            // Save to local pending queue
            var pending = new PendingPunch
            {
                EmployeeCode = punch.EmployeeCode,
                TimestampUtc = punch.TimestampUtc,
                Type = punch.Type,
                Notes = punch.Notes,
                Synced = false
            };

            await _localDb.InsertPendingPunchAsync(pending);
            return false;
        }

        // Force sync pending punches (called by SyncService)
        public async Task SyncPendingAsync()
        {
            var pending = await _localDb.GetAllPendingPunchesAsync();
            foreach (var p in pending)
            {
                try
                {
                    var dto = new PunchDto
                    {
                        EmployeeCode = p.EmployeeCode,
                        TimestampUtc = p.TimestampUtc,
                        Type = p.Type,
                        Notes = p.Notes
                    };

                    var resp = await _api.PostAsync("/api/punch", dto);

                    if (resp.IsSuccessStatusCode)
                    {
                        // mark as synced locally
                        await _localDb.MarkPendingAsSyncedAsync(p.LocalId);
                    }
                }
                catch
                {
                    // stop or continue depending on policy; continue to next to avoid total halt
                    continue;
                }
            }
        }
    }
}
