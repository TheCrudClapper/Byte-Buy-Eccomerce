using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRentOfferService : IBaseService
{
    Task<Result<CreatedResponse>> Add(RentOfferAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, RentOfferUpdateRequest request);
    Task<Result<RentOfferResponse>> GetById(Guid id);
    Task<Result<IEnumerable<RentOffer>>> GetList();
}
