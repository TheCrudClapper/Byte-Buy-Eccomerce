using ByteBuy.Core.DTO;
using ByteBuy.Core.ResultTypes;


namespace ByteBuy.Core.ServiceContracts.Base;

/// <summary>
/// Defines basic contract for crud services for standarized API
/// </summary>
/// <typeparam name="TId">Type used for identifying entitiy, model</typeparam>
/// <typeparam name="TAddRequest">Dto used for adding entity, model</typeparam>
/// <typeparam name="TUpdateRequest">Dto used for updating entity, model</typeparam>
/// <typeparam name="TResponse">Dto used for returning entity, model with all data necessary to perform update</typeparam>
public interface IBaseCrudService<TId, TAddRequest, TUpdateRequest, TResponse>
{
    Task<Result<CreatedResponse>> AddAsync(TAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(TId id, TUpdateRequest request);
    Task<Result> DeleteAsync(TId id);
    Task<Result<TResponse>> GetById(TId id,  CancellationToken ct = default);
}
