using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;
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

    public static ExistingImageUpdate ToExistingImageUpdate(this ExistingImageUpdateRequest dto)
    {
        return new ExistingImageUpdate(dto.Id, dto.AltText, dto.IsDeleted);
    }

    public static ImageDraft ToImageDraft(this SavedImage image)
    {
        return new ImageDraft(image.ImagePath, image.AltText);
    }
}

