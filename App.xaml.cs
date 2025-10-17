using Microsoft.Maui.Controls;

namespace PunchKioskMobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
