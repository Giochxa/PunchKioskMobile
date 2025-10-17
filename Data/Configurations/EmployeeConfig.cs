using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PunchKioskMobile.Models;

namespace PunchKioskMobile.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.EmployeeCode).IsRequired().HasMaxLength(50);
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Position).HasMaxLength(100);
            builder.Property(e => e.Department).HasMaxLength(100);
            builder.Property(e => e.PhotoUrl).HasMaxLength(500);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired();

            builder.HasIndex(e => e.EmployeeCode).IsUnique();

            builder.HasMany(e => e.Punches)
                   .WithOne(p => p.Employee)
                   .HasForeignKey(p => p.EmployeeId);
        }
    }
}