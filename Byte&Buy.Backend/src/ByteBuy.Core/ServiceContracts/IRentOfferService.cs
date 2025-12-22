using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.RentOffer;
using ByteBuy.Core.DTO.SaleOffer;
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

