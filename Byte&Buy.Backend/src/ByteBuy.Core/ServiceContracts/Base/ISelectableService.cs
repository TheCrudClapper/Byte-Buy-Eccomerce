using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts.Base;

/// <summary>
/// Represents a contract that allows a service return data in select list format
/// with Title and Id for Comboboxes, selects etc.
/// </summary>
/// <typeparam name="TId">Type of Id used for value</typeparam>
public interface ISelectableService<TId>
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<TId>>>> GetSelectListAsync(CancellationToken ct = default);
}
