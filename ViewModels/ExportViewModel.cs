using System.ComponentModel.DataAnnotations;

namespace PunchKioskMobile.ViewModels
{
    public class ExportViewModel : BaseViewModel
    {
        private DateTime _startDate = DateTime.Today.AddDays(-7);
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private bool _isExporting;
        public bool IsExporting
        {
            get => _isExporting;
            set => SetProperty(ref _isExporting, value);
        }

        private string _exportResult;
        public string ExportResult
        {
            get => _exportResult;
            set => SetProperty(ref _exportResult, value);
        }

        private bool _showSuccess;
        public bool ShowSuccess
        {
            get => _showSuccess;
            set => SetProperty(ref _showSuccess, value);
        }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDateRequired
        {
            get => StartDate;
            set => StartDate = value ?? DateTime.Today.AddDays(-7);
        }

        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDateRequired
        {
            get => EndDate;
            set => EndDate = value ?? DateTime.Today;
        }

        [CustomValidation(typeof(ExportViewModel), nameof(ValidateDateRange))]
        public DateTime? DateRangeValidation => StartDate;
    }
}