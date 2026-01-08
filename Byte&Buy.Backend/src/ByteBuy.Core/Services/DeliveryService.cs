using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.DeliverySpecifications;

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
    public async Task<Result<CreatedResponse>> AddAsync(DeliveryAddRequest request)
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

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, DeliveryUpdateRequest request)
    {
        var exist = await _deliveryRepository.ExistWithNameAsync(request.Name, id);
        if (exist)
            return Result.Failure<UpdatedResponse>(DeliveryErrors.AlreadyExists);

        var carrierExists = await _carrierRepository.ExistsByCondition(dc => dc.Id == request.CarrierId);
        if (!carrierExists)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.NotFound);

        var delivery = await _deliveryRepository.GetByIdAsync(id);
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

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _deliveryRepository.HasActiveRelations(id))
            return Result.Failure(DeliveryErrors.HasActiveRelations);

        var delivery = await _deliveryRepository.GetByIdAsync(id);
        if (delivery is null)
            return Result.Failure(Error.NotFound);

        delivery.Deactivate();

        await _deliveryRepository.UpdateAsync(delivery);
        await _deliveryRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result<DeliveryResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(id, ct);
        return delivery is null
            ? Result.Failure<DeliveryResponse>(Error.NotFound)
            : delivery.ToDeliveryResponse();
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct = default)
    {
        var spec = new DeliveryToSelectListItemSpec();
        return await _deliveryRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListAsync(CancellationToken ct = default)
    {
        var spec = new DeliveryToDeliveryListResponseSpec();
        return await _deliveryRepository.GetListBySpecAsync(spec, ct);
    }

    public Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels()
        => Result.Success(EnumToSelectListMapper.EnumToSelectLists<DeliveryChannelEnum>());

    public Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes()
        => Result.Success(EnumToSelectListMapper.EnumToSelectLists<ParcelSizeEnum>());

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync(CancellationToken ct)
    {
        var spec = new DeliveryToDeliveryOptionResponseSpec();
        var deliveries = await _deliveryRepository.GetListBySpecAsync(spec, ct);

        var response = new DeliveryOptionsResponse
        {
            ParcelLockerDeliveries = DeliveryMappings.MapDeliveries(deliveries, DeliveryChannelEnum.ParcelLocker),
            CourierDeliveries = DeliveryMappings.MapDeliveries(deliveries, DeliveryChannelEnum.Courier),
            PickupPointDeliveries = DeliveryMappings.MapDeliveries(deliveries, DeliveryChannelEnum.PickupPoint)
        };

        return response;
    }

    //Helpers
    private static ParcelSizeEnum? GetEnumOrNull(int? id)
    {
        return id.HasValue
            ? (ParcelSizeEnum)id.Value
            : null;
    }
}
