using GuardingChild.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuardingChild.Data.Configurations
{
    public class KidConfig : IEntityTypeConfiguration<Kid>
    {
        public void Configure(EntityTypeBuilder<Kid> builder)
        {
            builder.Property(p => p.First_Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Last_Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.SSN)
                .IsRequired();
            builder.HasIndex(p => p.SSN)
                .IsUnique();
            builder.Property(p => p.Gender)
                .HasConversion<string>() // store as text
                .HasMaxLength(10);

        }
    }
}
