namespace ByteBuy.Core.DTO.Internal.Checkout;

public record SellerCheckoutResponse(
    Guid SellerId,
    string SellerDisplayName,
    string SellerEmail);
