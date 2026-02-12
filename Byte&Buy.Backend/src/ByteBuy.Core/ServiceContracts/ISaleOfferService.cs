using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.SaleOffer;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ISaleOfferService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, SaleOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery queryParams, CancellationToken ct = default);
}
