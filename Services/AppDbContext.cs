using Microsoft.EntityFrameworkCore;
using PunchKioskMobile.Models;
using Microsoft.Maui.Storage;

namespace PunchKioskMobile.Services;

public class AppDbContext : DbContext
{
    public DbSet<Punch> Punches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "punchkiosk.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
