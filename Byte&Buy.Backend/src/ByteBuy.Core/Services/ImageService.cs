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

    public Result DeleteImages(IList<string> imagePaths, ImageTypeEnum type)
    {
        if (imagePaths.Count > 0)
        {
            var deletedResult = _imageStorage.DeleteFromDirectory(imagePaths, type);
            if (deletedResult.IsFailure)
                return Result.Failure(deletedResult.Error);
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
    public async Task<Result<IReadOnlyList<SavedImage>>> SaveNewImagesAsync<TImageDto>(IEnumerable<TImageDto>? images, ImageTypeEnum imageType)
        where TImageDto : IImageRequestDto
    {
        if (images is null || !images.Any())
            return Result.Success<IReadOnlyList<SavedImage>>([]);

        var newImages = images.ToList();

        var files = newImages
            .Select(i => i.Image)
            .ToList();

        var imageSaveResult = await _imageStorage.SaveToDirectoryAsync(files, imageType);
        if (imageSaveResult.IsFailure)
            return Result.Failure<IReadOnlyList<SavedImage>>(imageSaveResult.Error);

        IList<SavedImage> savedImages = [];

        for (int i = 0; i < newImages.Count; i++)
        {
            var path = imageSaveResult.Value[i];
            var altText = newImages[i].AltText;
            savedImages.Add(new SavedImage(path, altText));
        }

        return savedImages.AsReadOnly();
    }

    public void RollbackImageSave(IList<string> imagePaths, ImageTypeEnum type)
        => _imageStorage.DeleteFromDirectory(imagePaths, type);
}
