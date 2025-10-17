// ViewModels/PunchViewModel.cs
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PunchKioskMobile.Models;
using PunchKioskMobile.Services;
using Microsoft.Maui.Controls; // MAUI uses Microsoft.Maui.Controls but Command exists in both; if using MAUI use Microsoft.Maui.Controls

namespace PunchKioskMobile.ViewModels
{
    public class PunchViewModel : BaseViewModel
    {
        private readonly PunchService _punchService;
        private string _employeeCode;
        private string _statusMessage;

        public string EmployeeCode
        {
            get => _employeeCode;
            set => Set(ref _employeeCode, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => Set(ref _statusMessage, value);
        }

        public ICommand PunchInCommand { get; }
        public ICommand PunchOutCommand { get; }

        public PunchViewModel(PunchService punchService)
        {
            _punchService = punchService;
            PunchInCommand = new Command(async () => await ExecutePunchAsync("IN"));
            PunchOutCommand = new Command(async () => await ExecutePunchAsync("OUT"));
        }

        private async Task ExecutePunchAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(EmployeeCode))
            {
                StatusMessage = "Enter employee code";
                return;
            }

            var punch = new PunchDto
            {
                EmployeeCode = EmployeeCode.Trim(),
                TimestampUtc = DateTime.UtcNow,
                Type = type,
                Notes = null
            };

            var sent = await _punchService.SubmitPunchAsync(punch);

            StatusMessage = sent ? $"{type} recorded online" : $"{type} saved locally (will sync)";
            EmployeeCode = string.Empty;
        }
    }
}
