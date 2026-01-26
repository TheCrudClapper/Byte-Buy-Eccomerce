using ByteBuy.Core.Domain.Entities;
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

        builder.OwnsOne(o => o.Lender, sa =>
        {
            sa.Property(prop => prop.Type)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            sa.Property(prop => prop.Id)
                .IsRequired();
        });


        builder.OwnsOne(r => r.PricePerDay, m =>
        {
            m.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
            m.Property(prop => prop.Amount).HasPrecision(18,3).IsRequired();
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}