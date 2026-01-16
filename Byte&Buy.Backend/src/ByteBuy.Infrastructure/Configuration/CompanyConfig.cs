using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");
        builder.OwnsOne(ci => ci.CompanyAddress, ci =>
        {
            ci.Property(prop => prop.City).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.Street).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.Country).HasMaxLength(50).IsRequired();
            ci.Property(prop => prop.PostalCode).HasMaxLength(50).IsRequired();
            ci.Property(p => p.PostalCity).HasMaxLength(50);
            ci.Property(prop => prop.HouseNumber).HasMaxLength(20).IsRequired();
        });

        builder.Property(ci => ci.CompanyName).HasMaxLength(50).IsRequired();
        builder.Property(ci => ci.TIN).HasMaxLength(20).IsRequired();
        builder.Property(ci => ci.Slogan).HasMaxLength(30);
        builder.Property(ci => ci.Phone).HasMaxLength(16).IsRequired();
        builder.Property(ci => ci.Email).HasMaxLength(50).IsRequired();

    }
}
