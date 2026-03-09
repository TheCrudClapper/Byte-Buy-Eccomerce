using ByteBuy.Core.Domain.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasOne(p => p.Borrower)
            .WithMany(pu => pu.Rentals)
            .HasForeignKey(p => p.BorrowerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(p => p.Thumbnail, i =>
        {
            i.Property(img => img.AltText).HasMaxLength(50);
            i.Property(img => img.ImagePath);
        });

        builder.OwnsOne(o => o.Lender, ss =>
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

        builder.OwnsOne(r => r.PricePerDay, m =>
        {
            m.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
            m.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}