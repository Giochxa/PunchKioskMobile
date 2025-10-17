using Microsoft.EntityFrameworkCore;
using PunchKioskMobile.Data;

namespace PunchKioskMobile
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            InitializeDatabase(serviceProvider);

            MainPage = new AppShell();
        }

        private async void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            await SeedInitialData(context);
        }

        private async Task SeedInitialData(AppDbContext context)
        {
            if (!await context.Employees.AnyAsync())
            {
                var employees = new List<Employee>
                {
                    new Employee { EmployeeCode = "EMP001", FullName = "John Smith", Position = "Manager", Department = "Administration", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Employee { EmployeeCode = "EMP002", FullName = "Jane Doe", Position = "Developer", Department = "IT", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Employee { EmployeeCode = "EMP003", FullName = "Mike Johnson", Position = "Analyst", Department = "Finance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                };

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}