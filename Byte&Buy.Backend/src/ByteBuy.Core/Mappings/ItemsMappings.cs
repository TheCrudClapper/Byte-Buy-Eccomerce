using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Item;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class ItemsMappings
{
    public static Expression<Func<Item, ItemListResponse>> ItemListResponseProjection =>
        i => new ItemListResponse(
            i.Id,
            i.Name,
            i.Images.Count,
            i.StockQuantity,
            i.Condition.Name,
            i.Category.Name
            );

    public static Expression<Func<Item, ItemResponse>> ItemResponseProjection =>
        i => new ItemResponse(
            i.Id,
            i.CategoryId,
            i.ConditionId,
            i.Name,
            i.Description,
            i.StockQuantity,
            i.Images.AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .ToList()
            );
}
