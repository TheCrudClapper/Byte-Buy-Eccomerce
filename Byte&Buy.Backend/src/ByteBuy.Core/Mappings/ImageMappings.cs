using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Image;
using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class ImageMappings
{
    public static ImageResponse ToImageResponse(this Image image)
        => new ImageResponse(image.Id, image.ImagePath, image.AltText);
    

    public static Expression<Func<Image, ImageResponse>> ImageResponseProjection =>
       i => new ImageResponse(
           i.Id,
           i.ImagePath,
           i.AltText);

    public static Expression<Func<Image, ImageThumbnailDto>> ImageThumbnailProjection =>
        i => new ImageThumbnailDto(i.ImagePath, i.AltText);

    public static ExistingImageUpdate ToExistingImageUpdate(this ExistingImageUpdateRequest dto)
        => new ExistingImageUpdate(dto.Id, dto.AltText, dto.IsDeleted);
    
    public static ImageDraft ToImageDraft(this SavedImage image)
        => new ImageDraft(image.ImagePath, image.AltText);
    
}

