namespace ByteBuy.Core.DTO.Delivery
{
    public record DeliveryListResponse(
        Guid Id,
        string Name,
        string Currency,
        decimal Amount,
        string Carrier
        );
}
