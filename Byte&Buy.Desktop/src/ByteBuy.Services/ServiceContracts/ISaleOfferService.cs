using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ISaleOfferService : IBaseService
{
    Task<Result<CreatedResponse>> Add(SaleOfferAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, SaleOfferUpdateRequest request);
    Task<Result<SaleOfferResponse>> GetById(Guid id);
    Task<Result<IReadOnlyCollection<SaleOfferListResponse>>> GetList();
}
