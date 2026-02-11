using ByteBuy.Core.DTO.Internal.DocumentModels;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDocumentRepository
{
    Task<OrderDetailsPdfModel> GetOrderDetails(Guid orderId);
}
