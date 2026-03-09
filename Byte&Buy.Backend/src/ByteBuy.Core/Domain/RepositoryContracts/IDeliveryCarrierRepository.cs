using ByteBuy.Core.Domain.DeliveryCarriers;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.Filtration.DeliveryCarrier;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryCarrierRepository : IRepositoryBase<DeliveryCarrier>
{
    Task<bool> HasActiveRelationsAsync(Guid carrierId);
    Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? exludeId = null);
    Task<IReadOnlyCollection<DeliveryCarrier>> GetAllAsync(CancellationToken ct = default);
    Task<PagedList<DeliveryCarrierResponse>> GetDeliveryCarrierListAsync(DeliveryCarriersListQuery queryParams, CancellationToken ct = default);
}
