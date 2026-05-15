using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;
using MiniEMR.Enums;

namespace MiniEMR.Configuration
{
    public class DrugConfiguration : IEntityTypeConfiguration<Drug>
    {
        public void Configure(EntityTypeBuilder<Drug> builder)
        {
            builder.Property(d => d.DrugForm)
                .HasConversion<string>()
                 .HasMaxLength(30)
                .IsRequired();

            builder.HasKey(d => d.DrugId);

            builder.Property(d => d.DrugName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.GenericName)
                   .HasMaxLength(100);

            builder.Property(d => d.Strength)
                   .HasMaxLength(50);

            builder.Property(d => d.DrugForm)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(d => d.Manufacturer)
                   .HasMaxLength(100);

            builder.Property(d => d.IsActive)
                   .HasDefaultValue(true);

        }
    }
}
