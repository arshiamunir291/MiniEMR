using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;
using MiniEMR.Enums;

namespace MiniEMR.Configuration
{
    public class VisitConfiguration:IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visits", t =>
            {
                t.HasCheckConstraint(
                    "CK_Visit_VisitType",
                    $"VisitType IN ('{VisitType.Appointment}','{VisitType.Emergency}')");
            });

            builder.HasKey(v => v.VisitId);

            builder.HasIndex(x => x.AppointmentId)
             .IsUnique()
             .HasFilter("[AppointmentId] IS NOT NULL");

            builder.Property(v => v.AppointmentId)
                   .IsRequired(false);

            builder.Property(v => v.PatientId)
                   .IsRequired();

            builder.Property(v => v.DoctorId)
                   .IsRequired();

            builder.Property(v => v.VisitType)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(v => v.ChiefComplaint)
                   .IsRequired();

            builder.Property(v => v.VisitsNotes)
                   .IsRequired();

            builder.Property(v => v.Diagnosis)
                   .IsRequired();

            builder.Property(v => v.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasOne(v => v.Patient)
                   .WithMany(p=>p.Visits)
                   .HasForeignKey(v => v.PatientId);

            builder.HasOne(v => v.Doctor)
                   .WithMany()
                   .HasForeignKey(v => v.DoctorId);


            builder.HasOne(v => v.Appointment)
                   .WithOne(a=>a.Visit)
                   .HasForeignKey<Visit>(v => v.AppointmentId);
            builder.Property(v => v.FollowUpInstructions)
                    .HasMaxLength(500);


        }
    }
}
