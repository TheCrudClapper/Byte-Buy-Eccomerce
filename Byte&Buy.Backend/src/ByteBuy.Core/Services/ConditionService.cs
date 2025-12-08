using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class ConditionService : IConditionService
{
    private readonly IConditionRepository _conditionRepository;

    public ConditionService(IConditionRepository conditionRepository)
    {
        _conditionRepository = conditionRepository;
    }

    public async Task<Result<CreatedResponse>> AddCondition(ConditionAddRequest request)
    {
        var result = Condition.Create(request.Name, request.Description);
        if (result.IsFailure)
            return Result.Failure<CreatedResponse>(result.Error);

        var condition = result.Value;
        await _conditionRepository.AddAsync(condition);

        return condition.ToCreatedResponse();
    }

    public async Task<Result> DeleteCondition(Guid conditionId)
    {
        var condition = await _conditionRepository.GetByIdAsync(conditionId);
        if (condition is null)
            return Result.Failure(Error.NotFound);

        condition.Deactivate();
        await _conditionRepository.UpdateAsync(condition);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ConditionResponse>>> GetConditions(CancellationToken ct = default)
    {
        var conditions = await _conditionRepository.GetAllAsync(ct);
        return conditions.Select(c => c.ToConditionResponse())
            .ToList();
    }

    public async Task<Result<ConditionResponse>> GetCondition(Guid conditionId, CancellationToken ct = default)
    {
        var condition = await _conditionRepository.GetByIdAsync(conditionId, ct);
        return condition is null
            ? Result.Failure<ConditionResponse>(Error.NotFound)
            : condition.ToConditionResponse();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default)
    {
        var conditions = await _conditionRepository.GetAllAsync(ct);
        return conditions.Select(c => c.ToSelectListItemResponse()).ToList();
    }

    public async Task<Result<UpdatedResponse>> UpdateCondition(Guid conditionId, ConditionUpdateRequest request)
    {
        var condition = await _conditionRepository.GetByIdAsync(conditionId);
        if (condition is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        condition.Update(request.Name, request.Description);
        await _conditionRepository.UpdateAsync(condition);

        return condition.ToUpdatedResponse();
    }
}
