using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class ItemHelperService : IItemHelperService
{
    private readonly IConditionRepository _conditionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IImageService _imageService;

    public ItemHelperService(IConditionRepository conditionRepository,
        ICategoryRepository categoryRepository,
        IImageService imageService)
    {
        _categoryRepository = categoryRepository;
        _conditionRepository = conditionRepository;
        _imageService = imageService;
    }

    public async Task<Result> ValidateCategoryAndCondition(Guid categoryId, Guid conditionId)
    {
        if (!await _categoryRepository.ExistsByCondition(cat => cat.Id == categoryId))
            return Result.Failure(CategoryErrors.NotFound);

        if (!await _conditionRepository.ExistsByCondition(con => con.Id == conditionId))
            return Result.Failure(ConditionErrors.NotFound);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<ImageDraft>>> SaveImageAndCreateDrafts(IEnumerable<ImageAddRequest>? newImages)
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
}
