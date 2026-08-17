using ByteBuy.Core.DTO.Public.Offer.SaleOffer;

namespace ByteBuy.Core.ServiceContracts;

public interface IUserSaleOfferService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, UserSaleOfferAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid userid, Guid id, UserSaleOfferUpdateRequest request);
    Task<Result> DeleteAsync(Guid userId, Guid id);
    Task<Result<UserSaleOfferResponse>> GetByIdAsync(Guid userId, Guid id, CancellationToken ct = default);
}
