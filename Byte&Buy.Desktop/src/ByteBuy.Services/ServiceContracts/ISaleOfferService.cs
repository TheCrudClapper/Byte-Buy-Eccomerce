using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ISaleOfferService : IBaseService
{
    Task<Result<CreatedResponse>> AddAsync(SaleOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request);
    Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery query);
}
