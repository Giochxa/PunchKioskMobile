using Microsoft.EntityFrameworkCore;

public class AppDbContext1 : DbContext
{
    public AppDbContext1(DbContextOptions<AppDbContext1> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<PunchRecord> PunchRecords { get; set; }
}
