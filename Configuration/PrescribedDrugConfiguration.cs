using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;

namespace MiniEMR.Configuration
{
    public class PrescribedDrugConfiguration:IEntityTypeConfiguration<PrescribedDrug>
    {
        public void Configure(EntityTypeBuilder<PrescribedDrug> builder)
        {
            builder.ToTable("PrescribedDrugs");
            builder.HasKey(p => p.PrescriptionId);

            builder.Property(p => p.Dosage)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Frequency)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.Duration)
                   .IsRequired();


            builder.Property(p => p.Instructions)
                   .HasMaxLength(500);

            builder.HasOne(p => p.Visit)
                   .WithMany(v => v.PrescribedDrugs)
                   .HasForeignKey(p => p.VisitId);

            builder.HasOne(p => p.Drug)
                   .WithMany(d => d.PrescribedDrugs)
                   .HasForeignKey(p => p.DrugId);

        }
    }
}
