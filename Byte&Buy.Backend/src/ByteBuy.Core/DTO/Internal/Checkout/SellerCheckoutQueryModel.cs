namespace ByteBuy.Core.DTO.Internal.Checkout;

public record SellerCheckoutQueryModel(
    Guid SellerId,
    string SellerDisplayName,
    string SellerEmail);
