using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryRepository : IRepositoryBase<Delivery>
{
    Task<bool> HasActiveRelations(Guid deliveryId);
    Task<bool> ExistWithNameAsync(string name, Guid? exludeId = null);
    Task<IReadOnlyCollection<Delivery>> GetAllByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    Task<List<Money>> GetCheapestCostByOfferIds(IEnumerable<Guid> offerIds);
    Task<PagedList<DeliveryListResponse>> GetDeliveriesListAsync(DeliveryListQuery queryParams, CancellationToken ct = default);
}
