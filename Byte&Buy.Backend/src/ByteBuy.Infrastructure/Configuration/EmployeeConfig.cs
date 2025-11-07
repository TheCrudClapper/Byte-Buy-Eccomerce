using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(e => e.CompanyInfo)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyInfoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(e => e.HomeAddress, a =>
        {
            a.Property(p => p.City).HasMaxLength(50);
            a.Property(p => p.Street).HasMaxLength(50);
            a.Property(p => p.PostalCode).HasMaxLength(20);
            a.Property(p => p.HouseNumber).HasMaxLength(10);
            a.Property(p => p.Country).HasMaxLength(50);
            a.Property(p => p.FlatNumber).HasMaxLength(10);
        });
    }
}
