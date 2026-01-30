namespace ByteBuy.Core.DTO.Public.Payment;

public record CardPaymentRequest(
    string CardHolderName,
    string CardNumber);
