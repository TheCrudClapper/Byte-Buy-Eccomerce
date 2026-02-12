using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IRentOfferHttpClient
{
    Task<Result<RentOfferResponse>> GetByIdAsync(Guid id);
    Task<Result<CreatedResponse>> PostRentOfferAsync(RentOfferAddRequest request);
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, RentOfferUpdateRequest request);
    Task<Result<PagedList<RentOfferListResponse>>> GetListAsync(RentOfferListQuery query);
}
