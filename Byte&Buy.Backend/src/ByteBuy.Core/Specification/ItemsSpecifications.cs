using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.Mappings;
namespace ByteBuy.Core.Specification;

public static class ItemsSpecifications
{
    public sealed class CompanyItemsToItemListResponseSpec : Specification<Item, ItemListResponse>
    {
        public CompanyItemsToItemListResponseSpec()
        {
            Query.AsNoTracking()
                 .Where(i => i.IsCompanyItem)
                 .Select(ItemsMappings.ItemListResponseProjection);
        }
    }
    public sealed class CompanyItemToItemResponseDtoSpec : Specification<Item, ItemResponse>
    {
        public CompanyItemToItemResponseDtoSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(i => i.Id == id && i.IsCompanyItem)
                 .Select(ItemsMappings.ItemResponseProjection);
        }
    }

}
