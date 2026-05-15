using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;

namespace MiniEMR.Configuration
{
    public class PrescriptionConfiguration:IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.PrescriptionId);

            builder.Property(p => p.Dosage)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Frequency)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.Duration)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Instructions)
                   .HasMaxLength(500);

            builder.HasOne(p => p.Visit)
                   .WithMany(v => v.Prescriptions)
                   .HasForeignKey(p => p.VisitId);

            builder.HasOne(p => p.Drug)
                   .WithMany(d => d.Prescriptions)
                   .HasForeignKey(p => p.DrugId);

        }
    }
}
