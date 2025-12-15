using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Image;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class ImageMappings
{
    public static Expression<Func<Image, ImageResponse>> ImageResponseProjection =>
       i => new ImageResponse(
           i.Id,
           i.ImagePath,
           i.AltText
           );
}
