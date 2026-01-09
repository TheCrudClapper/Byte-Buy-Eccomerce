using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class ItemValidationService : IItemValidationService
{
    private readonly IConditionRepository _conditionRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ItemValidationService(IConditionRepository conditionRepository, ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _conditionRepository = conditionRepository;
    }

    public async Task<Result> ValidateCategoryAndCondition(Guid categoryId, Guid conditionId)
    {
        if (!await _categoryRepository.ExistsByCondition(cat => cat.Id == categoryId))
            return Result.Failure(CategoryErrors.NotFound);

        if (!await _conditionRepository.ExistsByCondition(con => con.Id == conditionId))
            return Result.Failure(ConditionErrors.NotFound);

        return Result.Success();
    }
}
