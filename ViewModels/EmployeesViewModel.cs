// ViewModels/EmployeesViewModel.cs
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PunchKioskMobile.Models;
using PunchKioskMobile.Services;
using Microsoft.Maui.Controls;

namespace PunchKioskMobile.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private readonly EmployeeService _employeeService;
        public ObservableCollection<EmployeeDto> Employees { get; } = new ObservableCollection<EmployeeDto>();

        public ICommand RefreshCommand { get; }

        public EmployeesViewModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
            RefreshCommand = new Command(async () => await RefreshAsync());
        }

        public async Task RefreshAsync()
        {
            var list = await _employeeService.GetLocalEmployeesAsync();
            Employees.Clear();
            foreach (var e in list)
                Employees.Add(e);
        }
    }
}
