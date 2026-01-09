using ByteBuy.Core.Contracts;
using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Abstractions;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
namespace ByteBuy.Core.Services;

public class ImageService : IImageService
{
    private readonly IImageStorage _imageStorage;
    public ImageService(IImageStorage imageStorage)
    {
        _imageStorage = imageStorage;
    }

    public Result DeleteImagesPhysically(ItemUpdateRequest request, Item aggregate)
    {
        var deletedIds = request.ExistingImages
            .Where(i => i.IsDeleted)
            .Select(i => i.Id)
            .ToList();

        var deletedPaths = aggregate.GetImagePathsByIds(deletedIds);

        if (deletedPaths.Count > 0)
        {
            var deletedResult = _imageStorage.DeleteFromDirectory(deletedPaths);
            if (deletedResult.IsFailure)
                return Result.Failure(deletedResult.Error);
        }

        return Result.Success();
    }

    public Result UpdateOrMarkAsDeletedExistingImages(ItemUpdateRequest request, Item aggregate)
    {
        foreach (var image in request.ExistingImages)
        {
            if (image.IsDeleted)
                aggregate.DeleteImagesById(image.Id);
            else
            {
                var changeResult = aggregate.ChangeImageAltText(image.Id, image.AltText);
                if (changeResult.IsFailure)
                    return Result.Failure(changeResult.Error);
            }
        }

        return Result.Success();
    }

    /// <summary>
    /// Processes and saves a collection of new images for the specified item, associating each imageId with the item
    /// using its provided metadata.
    /// </summary>
    /// <typeparam name="TImageDto">The type of the imageId data transfer object. Must implement the IImageRequestDto interface.</typeparam>
    /// <param name="images">A list of imageId data transfer objects containing the imageId files and associated metadata to be added to the
    /// item. Cannot be null.</param>
    /// <param name="item">The item to which the images will be associated. Cannot be null.</param>
    /// <returns>A Result indicating whether the images were successfully saved and associated with the item. Returns a failure
    /// result if any imageId could not be saved or added.</returns>
    public async Task<Result<IReadOnlyList<SavedImage>>> SaveNewImagesAsync<TImageDto>(IList<TImageDto> images, ImageTypeEnum imageType) where TImageDto : IImageRequestDto
    {
        if (images is null || images.Count == 0)
            return Result.Success<IReadOnlyList<SavedImage>>([]);

        var files = images
            .Select(i => i.Image)
            .ToList();

        var imageSaveResult = await _imageStorage.SaveToDirectoryAsync(files, imageType);
        if (imageSaveResult.IsFailure)
            return Result.Failure<IReadOnlyList<SavedImage>>(imageSaveResult.Error);

        IList<SavedImage> savedImages = [];
        
        for(int i = 0; i < images.Count; i++)
        {
            var path = imageSaveResult.Value[i];
            var altText = images[i].AltText;
            savedImages.Add(new SavedImage(path, altText));
        }

        return savedImages.AsReadOnly();
    }

    public void RollbackImageSave(IList<string> imagePaths)
        => _imageStorage.DeleteFromDirectory(imagePaths);
}
