using GuardingChild.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuardingChild.Data.Configurations
{
    public class KidConfig : IEntityTypeConfiguration<Kid>
    {
        public void Configure(EntityTypeBuilder<Kid> builder)
        {
            builder.Property(k => k.First_Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(k => k.Last_Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(k => k.SSN)
                .IsRequired();
            builder.HasIndex(k => k.SSN)
                .IsUnique();
            builder.Property(k => k.Gender)
                .HasConversion<string>() // store as text
                .HasMaxLength(10);
        }
    }
}
