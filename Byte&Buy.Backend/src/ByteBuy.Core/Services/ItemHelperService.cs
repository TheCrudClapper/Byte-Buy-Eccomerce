using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Categories.Errors;
using ByteBuy.Core.Domain.Conditions.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.Shared.DomainModels;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class ItemHelperService : IItemHelperService
{
    private readonly IConditionRepository _conditionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IImageService _imageService;
    private readonly IDeliveryRepository _deliveryRepository;

    public ItemHelperService(IConditionRepository conditionRepository,
        ICategoryRepository categoryRepository,
        IImageService imageService,
        IDeliveryRepository deliveryRepository)
    {
        _categoryRepository = categoryRepository;
        _conditionRepository = conditionRepository;
        _imageService = imageService;
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Result> ValidateCategoryAndConditionAsync(Guid categoryId, Guid conditionId)
    {
        if (!await _categoryRepository.ExistsByCondition(cat => cat.Id == categoryId))
            return Result.Failure(CategoryErrors.NotFound);

        if (!await _conditionRepository.ExistsByCondition(con => con.Id == conditionId))
            return Result.Failure(ConditionErrors.NotFound);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<ImageDraft>>> SaveImageAndCreateDraftsAsync(IEnumerable<ImageAddRequest>? newImages)
    {
        if (newImages is null || !newImages.Any())
            return Array.Empty<ImageDraft>();

        var imagesResult = await _imageService.SaveNewImagesAsync(newImages, ImageTypeEnum.Items);
        if (imagesResult.IsFailure)
            return Result.Failure<IReadOnlyList<ImageDraft>>(imagesResult.Error);

        return imagesResult.Value
            .Select(x => x.ToImageDraft())
            .ToList();
    }

    public void RollbackImageSave(IList<string> paths)
    {
        _imageService.RollbackImageSave(paths, ImageTypeEnum.Items);
    }

    public async Task<Result> ValidateCountryConditonDeliveryAsync(Guid categoryId,
        Guid conditionId, IEnumerable<Guid>? parcelLockerDeliveries,
        IEnumerable<Guid> otherDeliveries)
    {
        var validation = await ValidateCategoryAndConditionAsync(categoryId, conditionId);

        if (validation.IsFailure)
            return Result.Failure(validation.Error);

        var validatedDeliveries = await DeliveryValidationHelper.ValidateAllDeliveriesAsync(
           parcelLockerDeliveries,
           otherDeliveries,
           _deliveryRepository);

        if (validatedDeliveries.IsFailure)
            return Result.Failure(validatedDeliveries.Error);

        return Result.Success();
    }

    public IEnumerable<Guid> MergeDeliveryIds(IEnumerable<Guid> otherDeliveries, IEnumerable<Guid>? parcelLockerDeliveries)
        => otherDeliveries.Concat(parcelLockerDeliveries ?? []);

}
