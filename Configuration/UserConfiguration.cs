using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEMR.Entities;
using MiniEMR.Enums;
namespace MiniEMR.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        { 
            builder.HasKey(u => u.UserId);

            builder.HasIndex(u => u.UserName)
                   .IsUnique();

            builder.Property(u => u.Role)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(u => u.Specialization)
                   .HasMaxLength(100);

            builder.Property(u => u.IsActive)
                   .HasDefaultValue(true);
         

        }
    }

}

