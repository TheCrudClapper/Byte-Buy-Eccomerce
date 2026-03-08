using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRentOfferService : IBaseService
{
    Task<Result<CreatedResponse>> AddAsync(RentOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RentOfferUpdateRequest request);
    Task<Result<RentOfferResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<RentOfferListResponse>>> GetListAsync(RentOfferListQuery query);
}
