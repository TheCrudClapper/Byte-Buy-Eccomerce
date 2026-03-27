using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Shared;

namespace ByteBuy.Core.ServiceContracts.Base;

/// <summary>
/// Represents a contract that allows a service return data in select list format
/// with Title and SellerId for Comboboxes, selects etc.
/// </summary>
/// <typeparam name="TId">Type of SellerId used for value</typeparam>
public interface ISelectableService<TId>
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<TId>>>> GetSelectListAsync(CancellationToken ct = default);
}
