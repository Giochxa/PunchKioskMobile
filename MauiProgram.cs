using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PunchKioskMobile.Data;
using PunchKioskMobile.Data.Repositories;
using PunchKioskMobile.Services;
using PunchKioskMobile.ViewModels;
using PunchKioskMobile.Views.Pages;

namespace PunchKioskMobile
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IPunchRepository, PunchRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPunchService, PunchService>();
            builder.Services.AddScoped<IExportService, ExportService>();

            builder.Services.AddTransient<PunchViewModel>();
            builder.Services.AddTransient<EmployeeViewModel>();

            builder.Services.AddTransient<PunchPage>();
            builder.Services.AddTransient<EmployeesPage>();

            builder.Services.AddScoped<IPlatformService, PlatformService>();
            builder.Services.AddTransient<ExportViewModel>();
            builder.Services.AddTransient<ExportPage>();


            return builder.Build();
        }
    }
}