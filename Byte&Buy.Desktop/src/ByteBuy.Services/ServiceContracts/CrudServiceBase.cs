using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public abstract class CrudServiceBase<TResponse, TAddRequest, TUpdateRequest>
{
    public abstract Task<Result<TResponse>> Add(TAddRequest request);
    public abstract Task<Result<TResponse>> Update(Guid id, TUpdateRequest request);
    public abstract Task<Result<IEnumerable<TResponse>>> GetAll();
    public abstract Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
    public abstract Task<Result<TResponse>> GetById(Guid id);
    public abstract Task<Result> DeleteById(Guid id);
}