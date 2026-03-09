using ByteBuy.Core.Domain.DomainModels;
using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

/// <summary>
/// Service that provides validation and helper methods to make operations on items, offers simpler.
/// </summary>
public interface IItemHelperService
{
    /// <summary>
    /// Validates category and condition.
    /// </summary>
    /// <param name="categoryId">Guid of category</param>
    /// <param name="conditionId">Guid of condition</param>
    /// <returns>Returns result determining whether validation was successfull or not</returns>
    Task<Result> ValidateCategoryAndConditionAsync(Guid categoryId, Guid conditionId);

    /// <summary>
    /// Validates and delegates saving images to another service
    /// </summary>
    /// <param name="newImages"></param>
    /// <returns>
    /// On success return IReadOnlyCollection with image draft objects containing metadata about saved images
    /// On failure returns Error details
    /// </returns>
    Task<Result<IReadOnlyList<ImageDraft>>> SaveImageAndCreateDraftsAsync(IEnumerable<ImageAddRequest>? newImages);

    /// <summary>
    /// Delegates saving images to image service
    /// </summary>
    /// <param name="paths">Image paths pointing to images that needs to be deleted</param>
    void RollbackImageSave(IList<string> paths);

    /// <summary>
    /// Validates category and condition as well as deliveries in one go
    /// </summary>
    /// <param name="categoryId">Guid of category</param>
    /// <param name="conditionId">Guid of condition</param>
    /// <param name="parcelLockerDeliveries">Parcel locker deliveries</param>
    /// <param name="otherDeliveries">Other deliveries that are not parcel lockers</param>
    /// <returns></returns>
    Task<Result> ValidateCountryConditonDeliveryAsync(Guid categoryId, Guid conditionId, IEnumerable<Guid>? parcelLockerDeliveries, IEnumerable<Guid> otherDeliveries);

    /// <summary>
    /// Small helper/util methods to merge parcel lockers and other delivery ids 
    /// </summary>
    /// <param name="otherDeliveries"></param>
    /// <param name="parcelLockerDeliveries"></param>
    /// <returns></returns>
    IEnumerable<Guid> MergeDeliveryIds(IEnumerable<Guid> otherDeliveries, IEnumerable<Guid>? parcelLockerDeliveries);
}
