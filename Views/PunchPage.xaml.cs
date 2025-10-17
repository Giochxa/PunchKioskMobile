using PunchKioskMobile.ViewModels;

namespace PunchKiosk.Mobile.Views;

public partial class PunchPage : ContentPage
{
    public PunchPage()
    {
        InitializeComponent();
        BindingContext = new PunchViewModel();
    }
}
