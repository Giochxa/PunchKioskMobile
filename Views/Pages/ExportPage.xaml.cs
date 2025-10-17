using PunchKioskMobile.ViewModels;

namespace PunchKioskMobile.Views.Pages
{
    public partial class ExportPage : ContentPage
    {
        private readonly ExportViewModel _viewModel;

        public ExportPage(ExportViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}