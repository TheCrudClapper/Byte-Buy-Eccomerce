using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IDeliveryCarrierRepository _carrierRepository;

    public DeliveryService(
        IDeliveryRepository deliveryRepository,
        IDeliveryCarrierRepository carrierRepository)
    {
        _deliveryRepository = deliveryRepository;
        _carrierRepository = carrierRepository;
    }

    public async Task<Result<CreatedResponse>> AddDelivery(DeliveryAddRequest request)
    {
        var exist = await _deliveryRepository.ExistWithNameAsync(request.Name);
        if (exist)
            return Result.Failure<CreatedResponse>(DeliveryErrors.AlreadyExists);

        var carrierExists = await _carrierRepository.ExistsByCondition(dc => dc.Id == request.CarrierId);
        if (!carrierExists)
            return Result.Failure<CreatedResponse>(DeliveryCarrierErrors.NotFound);

        var size = GetEnumOrNull(request.ParcelSizeId);

        var deliveryResult = Delivery.Create(
            request.Name,
            request.Description,
            request.Price,
            size,
            (DeliveryChannelEnum)request.ChannelId,
            request.CarrierId
        );

        if (deliveryResult.IsFailure)
            return Result.Failure<CreatedResponse>(deliveryResult.Error);

        var delivery = deliveryResult.Value;
        await _deliveryRepository.AddAsync(delivery);
        await _deliveryRepository.CommitAsync();
        return delivery.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateDelivery(Guid deliveryId, DeliveryUpdateRequest request)
    {
        var exist = await _deliveryRepository.ExistWithNameAsync(request.Name, deliveryId);
        if (exist)
            return Result.Failure<UpdatedResponse>(DeliveryErrors.AlreadyExists);

        var carrierExists = await _carrierRepository.ExistsByCondition(dc => dc.Id == request.CarrierId);
        if (!carrierExists)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.NotFound);

        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var size = GetEnumOrNull(request.ParcelSizeId);

        var deliveryResult = delivery.Update(
            request.Name,
            request.Description,
            request.Price,
            size,
            (DeliveryChannelEnum)request.ChannelId,
            request.CarrierId
        );

        if (deliveryResult.IsFailure)
            return Result.Failure<UpdatedResponse>(deliveryResult.Error);

        await _deliveryRepository.UpdateAsync(delivery);
        await _deliveryRepository.CommitAsync();
        return delivery.ToUpdatedResponse();
    }

    public async Task<Result> DeleteDelivery(Guid deliveryId)
    {
        if (await _deliveryRepository.HasActiveRelations(deliveryId))
            return Result.Failure(DeliveryErrors.InUse);

        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery is null)
            return Result.Failure(Error.NotFound);

        delivery.Deactivate();

        await _deliveryRepository.UpdateAsync(delivery);
        await _deliveryRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result<DeliveryResponse>> GetDelivery(Guid deliveryId, CancellationToken ct)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);
        return delivery is null
            ? Result.Failure<DeliveryResponse>(Error.NotFound)
            : delivery.ToDeliveryResponse();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
    {
        var deliveries = await _deliveryRepository.GetAllAsync(ct);
        return deliveries.Select(d => d.ToSelectListItemResponse())
            .ToList();
    }

    public async Task<Result<IEnumerable<DeliveryListResponse>>> GetDeliveriesList(CancellationToken ct = default)
    {
        var deliveries = await _deliveryRepository.GetAllAsync(ct);
        return deliveries.Select(d => d.ToDeliveryListResponse())
            .ToList().AsReadOnly();
    }

    private static ParcelSizeEnum? GetEnumOrNull(int? id)
    {
        return id.HasValue
            ? (ParcelSizeEnum)id.Value
            : null;
    }

    public Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels()
        => Result.Success(EnumToSelectListMapper.EnumToSelectLists<DeliveryChannelEnum>());

    public Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes()
        => Result.Success(EnumToSelectListMapper.EnumToSelectLists<ParcelSizeEnum>());
}
