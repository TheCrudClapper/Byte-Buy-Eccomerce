using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;

    public DeliveryService(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Result<DeliveryResponse>> AddDelivery(DeliveryAddRequest request)
    {
        var delivery = Delivery.Create(
            request.Name,
            request.Description,
            request.Price
        );

        if (delivery.IsFailure)
            return Result.Failure<DeliveryResponse>(delivery.Error);

        await _deliveryRepository.AddAsync(delivery.Value);

        return delivery.Value.ToDeliveryResponse();
    }

    public async Task<Result> DeleteDelivery(Guid deliveryId)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
            return Result.Failure(Error.NotFound);

        delivery.Deactivate();
        await _deliveryRepository.UpdateAsync(delivery);

        return Result.Success();
    }

    public async Task<Result<DeliveryResponse>> UpdateDelivery(Guid deliveryId, DeliveryUpdateRequest request)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
            return Result.Failure<DeliveryResponse>(Error.NotFound);

        delivery.Update(
            request.Name,
            request.Description,
            request.Price
        );

        await _deliveryRepository.UpdateAsync(delivery);

        return delivery.ToDeliveryResponse();
    }

    public async Task<Result<IEnumerable<DeliveryResponse>>> GetDeliveries(CancellationToken ct)
    {
        var deliveries = await _deliveryRepository.GetAllAsync(ct);
        return deliveries.Select(d => d.ToDeliveryResponse()).ToList();
    }

    public async Task<Result<DeliveryResponse>> GetDelivery(Guid deliveryId, CancellationToken ct)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);
        return delivery is null
            ? Result.Failure<DeliveryResponse>(Error.NotFound)
            : delivery.ToDeliveryResponse();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct)
    {
        var deliveries = await _deliveryRepository.GetAllAsync(ct);
        return deliveries.Select(d => d.ToSelectListItemResponse())
            .ToList();
    }


}
