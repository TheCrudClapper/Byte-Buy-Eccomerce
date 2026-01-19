using ByteBuy.Core.DTO.Cart;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICartService
{
    Task<Result<CartResponse>> GetCart(Guid userId, CancellationToken ct  = default);
    Task<Result> AddSaleCartOffer(Guid userId, SaleCartOfferAddRequest request);
    Task<Result> AddRentCartOffer(Guid userId, RentCartOfferAddRequest request);
    Task<Result> UpdateRentCartOffer(Guid userId, Guid cartItemId, RentCartOfferUpdateRequest request);
    Task<Result> UpdateSaleCartOffer(Guid userId, Guid cartItemId, SaleCartOfferUpdateRequest request);

    Task<Result> DeleteCartOffer(Guid userId, Guid cartItemId);
}
