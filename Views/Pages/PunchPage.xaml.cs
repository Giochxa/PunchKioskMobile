// Views/Pages/PunchPage.xaml.cs
using Microsoft.Maui.Controls;
using PunchKioskMobile.ViewModels;

namespace PunchKioskMobile.Views.Pages
{
    public partial class PunchPage : ContentPage
    {
        public PunchPage(PunchViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
