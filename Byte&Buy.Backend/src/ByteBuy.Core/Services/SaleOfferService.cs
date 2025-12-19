using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class SaleOfferService : ISaleOfferService
{
    public Task<Result<CreatedResponse>> AddAsync(SaleOfferAddRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SaleOfferResponse>> GetById(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
