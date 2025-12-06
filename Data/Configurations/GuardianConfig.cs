using GuardingChild.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuardingChild.Data.Configurations;

public class GuardianConfig : IEntityTypeConfiguration<Guardian>
{
    public void Configure(EntityTypeBuilder<Guardian> builder)
    {
        builder.Property(g => g.SSN_Father)
            .IsRequired();
        builder.Property(g=>g.Father_Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(g => g.SSN_Mother)
            .IsRequired();
        builder.Property(g => g.Mother_Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(g=>g.Address)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(g => g.Phone)
            .IsRequired();
        //guardian has many kids
        builder.HasMany(g=>g.Kids)
            .WithOne(k=>k.Guardian)
            .HasForeignKey(k=>k.GuardianId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}