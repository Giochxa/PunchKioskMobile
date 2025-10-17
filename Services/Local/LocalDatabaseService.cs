// Services/Local/LocalDatabaseService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PunchKioskMobile.Models;
using SQLite;

namespace PunchKioskMobile.Services.Local
{
    public class LocalDatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public LocalDatabaseService()
        {
            var dbPath = GetDbPath();
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<PendingPunch>().Wait();
            _db.CreateTableAsync<EmployeeLocal>().Wait();
        }

        private string GetDbPath()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folder, "punchkiosk_mobile.db3");
        }

        // PendingPunch operations
        public Task<int> InsertPendingPunchAsync(PendingPunch p) => _db.InsertAsync(p);
        public Task<List<PendingPunch>> GetAllPendingPunchesAsync() => _db.Table<PendingPunch>().Where(x => !x.Synced).ToListAsync();
        public async Task MarkPendingAsSyncedAsync(int localId)
        {
            var p = await _db.FindAsync<PendingPunch>(localId);
            if (p != null)
            {
                p.Synced = true;
                await _db.UpdateAsync(p);
            }
        }

        // Employee local caching (simple table)
        public async Task UpsertEmployeeAsync(EmployeeDto dto)
        {
            var existing = await _db.Table<EmployeeLocal>().Where(x => x.EmployeeCode == dto.EmployeeCode).FirstOrDefaultAsync();
            if (existing == null)
            {
                await _db.InsertAsync(new EmployeeLocal
                {
                    EmployeeCode = dto.EmployeeCode,
                    FullName = dto.FullName,
                    Position = dto.Position,
                    PhotoUrl = dto.PhotoUrl,
                    LastUpdatedUtc = dto.LastUpdatedUtc
                });
            }
            else
            {
                existing.FullName = dto.FullName;
                existing.Position = dto.Position;
                existing.PhotoUrl = dto.PhotoUrl;
                existing.LastUpdatedUtc = dto.LastUpdatedUtc;
                await _db.UpdateAsync(existing);
            }
        }

        public Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return _db.Table<EmployeeLocal>().ToListAsync()
                .ContinueWith(t =>
                {
                    var locals = t.Result;
                    var list = new List<EmployeeDto>();
                    foreach (var l in locals)
                    {
                        list.Add(new EmployeeDto
                        {
                            EmployeeCode = l.EmployeeCode,
                            FullName = l.FullName,
                            Position = l.Position,
                            PhotoUrl = l.PhotoUrl,
                            LastUpdatedUtc = l.LastUpdatedUtc
                        });
                    }
                    return list;
                });
        }

        // EmployeeLocal class for storing cache
        [Table("EmployeesLocal")]
        class EmployeeLocal
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string EmployeeCode { get; set; }
            public string FullName { get; set; }
            public string Position { get; set; }
            public string PhotoUrl { get; set; }
            public DateTime LastUpdatedUtc { get; set; }
        }
    }
}
