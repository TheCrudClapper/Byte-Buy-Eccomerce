using Ardalis.Specification;
using ByteBuy.Core.Domain.Items;
using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.Mappings;
namespace ByteBuy.Core.Specification;

public static class ItemsSpecifications
{
    public sealed class CompanyItemResponseSpec : Specification<Item, ItemResponse>
    {
        public CompanyItemResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(i => i.Id == id && i.IsCompanyItem)
                 .Select(ItemsMappings.ItemResponseProjection);
        }
    }

}
