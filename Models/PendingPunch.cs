// Models/PendingPunch.cs
using SQLite;
using System;

namespace PunchKioskMobile.Models
{
    public class PendingPunch
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }

        public string EmployeeCode { get; set; }
        public DateTime TimestampUtc { get; set; }
        public string Type { get; set; } // "IN" or "OUT"
        public string Notes { get; set; }
        public bool Synced { get; set; } // false until server confirms
    }
}
