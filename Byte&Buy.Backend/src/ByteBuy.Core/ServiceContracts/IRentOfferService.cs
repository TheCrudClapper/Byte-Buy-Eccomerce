using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IRentOfferService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, RentOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RentOfferUpdateRequest request);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<RentOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<RentOfferListResponse>>> GetListAsync(CancellationToken ct = default);
}

