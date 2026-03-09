using ByteBuy.Core.Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ByteBuy.Infrastructure.Configuration
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(3);
            builder.Property(c => c.Name).HasMaxLength(50);

            builder.HasQueryFilter(item => item.IsActive);
        }
    }
}
