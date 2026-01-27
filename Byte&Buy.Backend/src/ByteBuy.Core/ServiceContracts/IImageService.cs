using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.DTO.Internal.Image;
using ByteBuy.Core.DTO.Public.Abstractions;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IImageService
{
    Task<Result<IReadOnlyList<SavedImage>>> SaveNewImagesAsync<TImageDto>(
        IEnumerable<TImageDto>? images,
        ImageTypeEnum imageType)
        where TImageDto : IImageRequestDto;

    void RollbackImageSave(IList<string> imagePaths, ImageTypeEnum type);
    Result DeleteImages(IList<string> imagePaths, ImageTypeEnum type);
}
