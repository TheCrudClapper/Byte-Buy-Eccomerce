using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Shared;

namespace ByteBuy.Core.ServiceContracts;

public interface IUserRentOfferService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, UserRentOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid userid, Guid id, UserRentOfferUpdateRequest request);
    Task<Result> DeleteAsync(Guid userId, Guid id);
    Task<Result<UserRentOfferResponse>> GetByIdAsync(Guid userId, Guid id, CancellationToken ct = default);
}
