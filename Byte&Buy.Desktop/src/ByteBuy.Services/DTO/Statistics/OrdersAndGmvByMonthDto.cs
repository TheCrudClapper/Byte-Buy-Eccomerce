namespace ByteBuy.Services.DTO.Statistics;

public record OrdersAndGmvByMonthDto(
    int Year,
    int Month,
    int OrdersCount,
    decimal Gmv);
