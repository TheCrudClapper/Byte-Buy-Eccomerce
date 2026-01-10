using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IItemHelperService
{
    /// <summary>
    /// Validates category and condition.
    /// </summary>
    /// <param name="categoryId">Guid of category</param>
    /// <param name="conditionId">Guid of condition</param>
    /// <returns>Returns result determining whether validation was successfull or not</returns>
    Task<Result> ValidateCategoryAndCondition(Guid categoryId, Guid conditionId);

    /// <summary>
    /// Validates and delegates saving images to another service
    /// </summary>
    /// <param name="newImages"></param>
    /// <returns>
    /// On success return IReadOnlyCollection with image draft objects containing metadata about saved images
    /// On failure returns Error details
    /// </returns>
    Task<Result<IReadOnlyList<ImageDraft>>> SaveImageAndCreateDrafts(IEnumerable<ImageAddRequest>? newImages);

    /// <summary>
    /// Delegates saving images to image service
    /// </summary>
    /// <param name="paths">Image paths pointing to images that needs to be deleted</param>
    void RollbackImageSave(IList<string> paths);


}
