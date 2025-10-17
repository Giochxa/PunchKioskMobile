using System.ComponentModel.DataAnnotations;

namespace PunchKioskMobile.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee code is required")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}