using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;
using MiniEMR.Enums;

namespace MiniEMR.Configuration
{
    public class AppointmentConfiguration:IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments", t =>
            {
                t.HasCheckConstraint(
                    "CK_Appointment_Status",
                    $"Status IN (" +
                    $"'{AppointmentStatus.Scheduled}'," +
                    $"'{AppointmentStatus.CheckedIn}'," +
                    $"'{AppointmentStatus.Completed}'," +
                    $"'{AppointmentStatus.Cancelled}')");
            });

            builder.HasKey(a => a.AppointmentId);

            builder.Property(a => a.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(a => a.AppointmentDateTime)
                   .IsRequired();

            builder.Property(a => a.Notes)
                   .HasMaxLength(500);

            builder.Property(a => a.CancellationReason)
                   .HasMaxLength(500);

            builder.Property(a => a.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasIndex(a => new
            {
                a.DoctorId,
                a.AppointmentDateTime
            })
            .IsUnique()
            .HasFilter("[Status] IN ('Scheduled','CheckedIn')");

            builder.HasOne(a => a.Patient)
                   .WithMany()
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                   .WithMany()
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(a => a.CreatedBy);



            builder.HasOne(a => a.UpdatedByUser)
                   .WithMany()
                   .HasForeignKey(a => a.UpdatedBy);



            builder.HasOne(a => a.Visit)
                   .WithOne(v => v.Appointment)
                   .HasForeignKey<Visit>(v => v.AppointmentId);
                  


        }
    }
}
