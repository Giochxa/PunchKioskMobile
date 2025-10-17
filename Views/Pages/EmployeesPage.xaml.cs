// Views/Pages/EmployeesPage.xaml.cs
using Microsoft.Maui.Controls;
using PunchKioskMobile.ViewModels;

namespace PunchKioskMobile.Views.Pages
{
    public partial class EmployeesPage : ContentPage
    {
        private readonly EmployeesViewModel _vm;
        public EmployeesPage(EmployeesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.RefreshAsync();
        }
    }
}
