using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;

namespace MiniEMR.Configuration
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(
            EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.ToTable("DoctorSchedule");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DoctorId)
                .IsRequired();

            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.SlotDurationMinutes)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.HasOne(x => x.Doctor)
                 .WithMany(x => x.DoctorSchedules)
                 .HasForeignKey(x => x.DoctorId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x =>
                new
                {
                    x.DoctorId,
                    x.DayOfWeek
                });
        }

    }
}
