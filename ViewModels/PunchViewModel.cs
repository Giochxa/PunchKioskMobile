using System.ComponentModel.DataAnnotations;

namespace PunchKioskMobile.ViewModels
{
    public class PunchViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Employee code is required")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Punch type is required")]
        public string PunchType { get; set; }

        public string Notes { get; set; }

        public string PhotoUrl { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }

        public DateTime PunchTime { get; set; }

        public string StatusMessage { get; set; }

        public bool ShowSuccess { get; set; }

        public bool ShowError { get; set; }
    }
}