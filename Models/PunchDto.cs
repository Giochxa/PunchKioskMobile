// Models/PunchDto.cs
using System;

namespace PunchKioskMobile.Models
{
    public class PunchDto
    {
        public int Id { get; set; } // optional, assigned by backend
        public string EmployeeCode { get; set; } // the ID used by the kiosk
        public DateTime TimestampUtc { get; set; }
        public string Type { get; set; } // "IN" or "OUT"
        public string Notes { get; set; }
    }
}
