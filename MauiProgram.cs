using PunchKioskMobile.Platforms.Windows;
using PunchKioskMobile.Services;
using PunchKioskMobile.Services.Local;
using PunchKioskMobile.ViewModels;
using PunchKioskMobile.Views.Pages;

namespace PunchKioskMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Services
        builder.Services.AddSingleton<AppDbContext>();
       


        // Register HttpClient with ApiClient wrapper
        builder.Services.AddHttpClient<ApiClient>();

        // Local DB
        builder.Services.AddSingleton<LocalDatabaseService>();

        // API services
        builder.Services.AddSingleton<EmployeeService>();
        builder.Services.AddSingleton<PunchService>();

        // Sync service
        builder.Services.AddSingleton<SyncService>();

        // ViewModels
        builder.Services.AddSingleton<PunchViewModel>();
        builder.Services.AddSingleton<EmployeesViewModel>();

        // Pages (inject ViewModels into Pages' constructors)
        builder.Services.AddSingleton<PunchPage>();
        builder.Services.AddSingleton<EmployeesPage>();

        return builder.Build();

        var sync = app.Services.GetService<SyncService>();
        // start periodic sync every 60 seconds (or choose appropriate interval)
        sync?.StartPeriodicSync(60);
    }
}
