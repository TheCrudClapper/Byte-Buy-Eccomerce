using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class DocumentService : IDocumentService
{
    public Task<byte[]> GenerateOrderDetailsPdf(Guid orderId)
    {
        throw new NotImplementedException();
    }
}
