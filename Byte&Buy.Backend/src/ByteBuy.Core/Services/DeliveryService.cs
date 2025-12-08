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

    public async Task<Result<CreatedResponse>> AddDelivery(DeliveryAddRequest request)
    {
        var deliveryResult = Delivery.Create(
            request.Name,
            request.Description,
            request.Price
        );

        if (deliveryResult.IsFailure)
            return Result.Failure<CreatedResponse>(deliveryResult.Error);

        var delivery = deliveryResult.Value;
        await _deliveryRepository.AddAsync(deliveryResult.Value);

        return delivery.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateDelivery(Guid deliveryId, DeliveryUpdateRequest request)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        delivery.Update(
            request.Name,
            request.Description,
            request.Price
        );

        await _deliveryRepository.UpdateAsync(delivery);

        return delivery.ToUpdatedResponse();
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

    public async Task<Result<IEnumerable<DeliveryListResponse>>> GetDeliveriesList(CancellationToken ct = default)
    {
        var deliveries = await _deliveryRepository.GetAllAsync(ct);
        return deliveries.Select(d => d.ToDeliveryListResponse())
            .ToList();
    }

}
