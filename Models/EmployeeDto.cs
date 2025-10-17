// Models/EmployeeDto.cs
using System;

namespace PunchKioskMobile.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }             // backend employee id
        public string EmployeeCode { get; set; } // e.g. badge number or string id
        public string FullName { get; set; }
        public string Position { get; set; }
        public string PhotoUrl { get; set; }    // optional
        public DateTime LastUpdatedUtc { get; set; } // for synchronization
    }
}
