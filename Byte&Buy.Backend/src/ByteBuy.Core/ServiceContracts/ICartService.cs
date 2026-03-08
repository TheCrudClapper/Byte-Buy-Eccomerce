using ByteBuy.Core.DTO.Public.Cart;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICartService
{
    Task<Result<CartResponse>> GetCartAsync(Guid userId, CancellationToken ct = default);
    Task<Result> AddSaleCartOfferAsync(Guid userId, SaleCartOfferAddRequest request);
    Task<Result> AddRentCartOfferAsync(Guid userId, RentCartOfferAddRequest request);
    Task<Result<CartSummaryResponse>> UpdateRentCartOfferAsync(Guid userId, Guid cartItemId, RentCartOfferUpdateRequest request);
    Task<Result<CartSummaryResponse>> UpdateSaleCartOfferAsync(Guid userId, Guid cartItemId, SaleCartOfferUpdateRequest request);
    Task<Result<CartSummaryResponse>> DeleteCartOfferAsync(Guid userId, Guid cartItemId);
    Task<Result> ClearCartAsync(Guid userId);
}
