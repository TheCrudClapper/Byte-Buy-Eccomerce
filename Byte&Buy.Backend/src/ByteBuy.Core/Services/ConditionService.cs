using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class ConditionService : IConditionService
{
    private readonly IConditionRepository _conditionRepository;

    public ConditionService(IConditionRepository conditionRepository)
        => _conditionRepository = conditionRepository;

    public async Task<Result<CreatedResponse>> AddAsync(ConditionAddRequest request)
    {
        var exist = await _conditionRepository.ExistWithNameAsync(request.Name);
        if (exist)
            return Result.Failure<CreatedResponse>(ConditionErrors.AlreadyExists);

        var result = Condition.Create(request.Name, request.Description);
        if (result.IsFailure)
            return Result.Failure<CreatedResponse>(result.Error);

        var condition = result.Value;
        await _conditionRepository.AddAsync(condition);
        await _conditionRepository.CommitAsync();

        return condition.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ConditionUpdateRequest request)
    {
        var exist = await _conditionRepository.ExistWithNameAsync(request.Name, id);
        if (exist)
            return Result.Failure<UpdatedResponse>(ConditionErrors.AlreadyExists);

        var condition = await _conditionRepository.GetByIdAsync(id);
        if (condition is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        condition.Update(request.Name, request.Description);
        await _conditionRepository.UpdateAsync(condition);
        await _conditionRepository.CommitAsync();
        return condition.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _conditionRepository.HasActiveRelations(id))
            return Result.Failure(ConditionErrors.HasActiveItems);

        var condition = await _conditionRepository.GetByIdAsync(id);
        if (condition is null)
            return Result.Failure(Error.NotFound);

        condition.Deactivate();
        await _conditionRepository.UpdateAsync(condition);
        await _conditionRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<ConditionResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var condition = await _conditionRepository.GetByIdAsync(id, ct);
        return condition is null
            ? Result.Failure<ConditionResponse>(Error.NotFound)
            : condition.ToConditionResponse();
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    {
        var conditions = await _conditionRepository.GetAllAsync(ct);
        return conditions.Select(c => c.ToSelectListItemResponse()).ToList();
    }

    public async Task<Result<IReadOnlyCollection<ConditionResponse>>> GetConditionsAsync(CancellationToken ct = default)
    {
        var conditions = await _conditionRepository.GetAllAsync(ct);
        return conditions.Select(c => c.ToConditionResponse())
            .ToList();
    }

    public async Task<Result<IReadOnlyCollection<ConditionListResponse>>> GetConditionsListAsync(CancellationToken ct = default)
    {
        var condtions = await _conditionRepository.GetAllAsync(ct);
        return condtions.Select(c => c.ToConditionListResponse()).ToList();
    }
}
