using PunchKioskMobile.ViewModels;

namespace PunchKioskMobile.Views;

public partial class PunchPage : ContentPage
{
    public PunchPage()
    {
        InitializeComponent();
        BindingContext = new PunchViewModel();
    }
}
