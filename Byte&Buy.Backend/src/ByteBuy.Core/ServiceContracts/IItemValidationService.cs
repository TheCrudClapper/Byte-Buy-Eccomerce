using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IItemValidationService
{
    Task<Result> ValidateCategoryAndCondition(Guid categoryId, Guid conditionId);
}
