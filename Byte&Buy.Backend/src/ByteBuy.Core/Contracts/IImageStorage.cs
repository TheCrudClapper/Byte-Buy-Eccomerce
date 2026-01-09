using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Http;

namespace ByteBuy.Core.Contracts;

/// <summary>
/// Defines methods for saving, deleting, and rolling back image files in a storage directory based on image type.
/// </summary>
/// <remarks>Implementations of this interface are responsible for managing the persistence of image files,
/// including handling different image categories as specified by the image type. Methods support asynchronous and
/// synchronous operations for saving and deleting files. Rollback functionality is provided to remove files that were
/// previously saved, which can be useful in error handling scenarios.</remarks>
public interface IImageStorage
{
    /// <summary>
    /// Saves a collection of images to filesystem, in case of exceptions, deletes already save images.
    /// </summary>
    /// <param name="files">Image file</param>
    /// <param name="type">Type of image to save, determines saving path</param>
    /// <returns>Result object determining success or failure of operation</returns>
    Task<Result<List<string>>> SaveToDirectoryAsync(IReadOnlyList<IFormFile> files, ImageTypeEnum type);

    /// <summary>
    /// Deletes a collection of images from filesystem
    /// </summary>
    /// <param name="imagePaths">Relative image path, used in database</param>
    /// <param name="type">Type of image to save, determines saving pathe</param>
    /// <returns>Result object determining success or failure of operation</returns>
    Result DeleteFromDirectory(IEnumerable<string> imagePaths, ImageTypeEnum type);

    /// <summary>
    /// Rolls back(deletes) images saved pre
    /// </summary>
    /// <param name="imagePaths">Relative image path, used in database</param>
    /// <param name="type">Type of image to save, determines saving path</param>
    void RollbackSavedFiles(IEnumerable<string> imagePaths, ImageTypeEnum type);
}
