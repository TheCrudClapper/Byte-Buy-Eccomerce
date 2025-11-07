using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class CompanyInfoConfig : IEntityTypeConfiguration<CompanyInfo>
{
    public void Configure(EntityTypeBuilder<CompanyInfo> builder)
    {
        builder.OwnsOne(ci => ci.Address, ci =>
        {
            ci.Property(prop => prop.City).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.Street).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.Country).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.PostalCode).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.HouseNumber).HasMaxLength(20).IsRequired();
        });

        builder.Property(ci => ci.CompanyName).HasMaxLength(50).IsRequired();
        builder.Property(ci => ci.NIP).HasMaxLength(20).IsRequired();
        builder.Property(ci => ci.Phone).HasMaxLength(16).IsRequired();
        builder.Property(ci => ci.Email).HasMaxLength(50).IsRequired();
    }
}
