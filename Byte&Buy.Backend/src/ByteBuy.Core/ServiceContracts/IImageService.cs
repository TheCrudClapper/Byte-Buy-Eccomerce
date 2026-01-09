using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Abstractions;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IImageService
{
    Task<Result<IReadOnlyList<SavedImage>>> SaveNewImagesAsync<TImageDto>(
        IList<TImageDto> images,
        ImageTypeEnum imageType)
        where TImageDto : IImageRequestDto;

    void RollbackImageSave(IList<string> imagePaths);
    Result DeleteImagesPhysically(ItemUpdateRequest request, Item aggregate);
    Result UpdateOrMarkAsDeletedExistingImages(ItemUpdateRequest request, Item aggregate);
}
