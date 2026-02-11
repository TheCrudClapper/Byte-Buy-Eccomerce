namespace ByteBuy.Core.ServiceContracts;

public interface IDocumentService
{
    Task<byte[]> GenerateOrderDetailsPdf(Guid orderId, CancellationToken ct = default);
}
