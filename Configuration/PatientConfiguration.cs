using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;

namespace MiniEMR.Configuration
{
    public class PatientConfiguration:IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.PatientId);

            builder.HasIndex(p => p.MRN)
                   .IsUnique();

            builder.HasIndex(p => p.CNIC)
                   .IsUnique();

            builder.Property(p => p.MRN)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Gender)
                .HasConversion<string>()
                   .IsRequired();
                   

            builder.Property(p => p.DateOfBirth)
                   .IsRequired();

            builder.Property(p => p.CNIC)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(p => p.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(p => p.BloodGroup)
                   .HasMaxLength(5);

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(p => p.EmergencyContactName)
                   .HasMaxLength(100);

            builder.Property(p => p.EmergencyContactNumber)
                   .HasMaxLength(15);

            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            builder.HasOne(p => p.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(p => p.CreatedBy);


            builder.HasOne(p => p.UpdatedByUser)
                   .WithMany()
                   .HasForeignKey(p => p.UpdatedBy);

            builder.HasMany(p => p.Visits)
                    .WithOne(v => v.Patient)
                    .HasForeignKey(v => v.PatientId);
        }
    }
}
