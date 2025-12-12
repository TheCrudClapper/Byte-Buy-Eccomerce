using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class ItemService : IItemService
{
    public Task<Result<CreatedResponse>> Add(CountryAddRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CountryResponse>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UpdatedResponse>> Update(Guid id, CountryUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
