using ByteBuy.Core.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //enum -> string
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        // Relation with Buyer
        builder.HasOne(o => o.Buyer)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.BuyerId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation with line
        builder.HasMany(o => o.Lines)
            .WithOne(ol => ol.Order)
            .HasForeignKey(ol => ol.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        // Owned type for Seller Snapshot Value Object
        builder.OwnsOne(o => o.SellerSnapshot, ss =>
        {
            ss.Property(prop => prop.Type)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            ss.Property(prop => prop.SellerId)
                .IsRequired();

            ss.Property(prop => prop.TIN)
                .HasMaxLength(20);

            ss.Property(prop => prop.DisplayName)
              .HasMaxLength(100)
              .IsRequired();

            ss.OwnsOne(prop => prop.Address, a =>
            {
                a.Property(p => p.City).HasMaxLength(50);
                a.Property(p => p.Street).HasMaxLength(50);
                a.Property(p => p.PostalCode).HasMaxLength(20);
                a.Property(p => p.PostalCity).HasMaxLength(50);
                a.Property(p => p.HouseNumber).HasMaxLength(10);
                a.Property(p => p.Country).HasMaxLength(50);
                a.Property(p => p.FlatNumber).HasMaxLength(10);
            });
        });

        // Owned type for Buyer Snapshot Value Object
        builder.OwnsOne(o => o.BuyerSnapshot, bs =>
        {
            bs.Property(p => p.FullName)
                .HasMaxLength(150)
                .IsRequired();

            bs.Property(p => p.Email)
                .HasColumnName("BuyerSnapshot_Email")
                .HasMaxLength(150)
                .IsRequired();

            bs.Property(p => p.PhoneNumber)
                .HasColumnName("BuyerSnapshot_PhoneNumber")
                .HasMaxLength(15);

            bs.OwnsOne(p => p.Address, a =>
            {
                a.Property(p => p.City)
                    .HasColumnName("BuyerSnapshot_Address_City")
                    .HasMaxLength(50);

                a.Property(p => p.Street)
                    .HasColumnName("BuyerSnapshot_Address_Street")
                    .HasMaxLength(50);

                a.Property(p => p.PostalCode)
                    .HasColumnName("BuyerSnapshot_Address_PostalCode")
                    .HasMaxLength(20);

                a.Property(p => p.PostalCity)
                    .HasColumnName("BuyerSnapshot_Address_PostalCity")
                    .HasMaxLength(50);

                a.Property(p => p.HouseNumber)
                    .HasColumnName("BuyerSnapshot_Address_HouseNumber")
                    .HasMaxLength(10);

                a.Property(p => p.Country)
                    .HasColumnName("BuyerSnapshot_Address_Country")
                    .HasMaxLength(50);

                a.Property(p => p.FlatNumber)
                    .HasColumnName("BuyerSnapshot_Address_FlatNumber")
                    .HasMaxLength(10);
            });
        });


        builder.OwnsOne(o => o.LinesTotal, lt =>
        {
            lt.Property(p => p.Amount)
                .HasColumnName("LinesTotal_Amount")
                .HasPrecision(18, 3)
                .IsRequired();

            lt.Property(p => p.Currency)
                .HasColumnName("LinesTotal_Currency")
                .HasMaxLength(3)
                .IsRequired();
        });


        builder.OwnsOne(o => o.Total, t =>
        {
            t.Property(p => p.Amount)
                .HasColumnName("Total_Amount")
                .HasPrecision(18, 3)
                .IsRequired();

            t.Property(p => p.Currency)
                .HasColumnName("Total_Currency")
                .HasMaxLength(3)
                .IsRequired();
        });


        builder.HasQueryFilter(item => item.IsActive);
    }
}
