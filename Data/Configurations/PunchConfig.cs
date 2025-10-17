using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PunchKioskMobile.Models;

namespace PunchKioskMobile.Data.Configurations
{
    public class PunchConfig : IEntityTypeConfiguration<Punch>
    {
        public void Configure(EntityTypeBuilder<Punch> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PunchType).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Notes).HasMaxLength(500);
            builder.Property(p => p.PunchTime).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.DeviceInfo).HasMaxLength(200);
            builder.Property(p => p.Location).HasMaxLength(200);

            builder.HasOne(p => p.Employee)
                   .WithMany(e => e.Punches)
                   .HasForeignKey(p => p.EmployeeId);

            builder.HasIndex(p => new { p.EmployeeId, p.PunchTime });
            builder.HasIndex(p => p.PunchTime);
        }
    }
}