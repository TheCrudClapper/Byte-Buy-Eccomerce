using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Item;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class ImageMappings
{
    public static Expression<Func<Image, ImageResponse>> ImageResponseProjection =>
       i => new ImageResponse(
           i.Id,
           i.AltText
           );
}
