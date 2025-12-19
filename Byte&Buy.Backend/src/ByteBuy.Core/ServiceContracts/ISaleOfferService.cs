using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ISaleOfferService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, SaleOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<SaleOfferResponse>> GetById(Guid id, CancellationToken ct = default);
}
