using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IDocumentService
{
    Task<Result<byte[]>> GenerateOrderDetailsPdfAsync(Guid orderId, CancellationToken ct = default);
}
