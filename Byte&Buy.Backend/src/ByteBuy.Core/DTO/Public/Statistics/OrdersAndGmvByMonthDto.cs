namespace ByteBuy.Core.DTO.Public.Statistics;

public record OrdersAndGmvByMonthDto(
    int Year,
    int Month,
    int OrdersCount,
    decimal Gmv);