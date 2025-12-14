using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IItemService : IBaseService
{
    Task<Result<CreatedResponse>> Add(ItemAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, CountryUpdateRequest request);
    Task<Result<CountryResponse>> GetById(Guid id);
}
