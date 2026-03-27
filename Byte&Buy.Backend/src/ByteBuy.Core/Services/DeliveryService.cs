using ByteBuy.Core.Domain.Deliveries;
using ByteBuy.Core.Domain.Deliveries.Enums;
using ByteBuy.Core.Domain.Deliveries.Errors;
using ByteBuy.Core.Domain.DeliveryCarriers.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.DeliverySpecifications;

namespace ByteBuy.Core.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IDeliveryCarrierRepository _carrierRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeliveryService(
        IDeliveryRepository deliveryRepository,
        IDeliveryCarrierRepository carrierRepository,
        IUnitOfWork unitOfWork)
    {
        _deliveryRepository = deliveryRepository;
        _carrierRepository = carrierRepository;
        _unitOfWork = unitOfWork;
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
            (DeliveryChannel)request.ChannelId,
            request.CarrierId
        );

        if (deliveryResult.IsFailure)
            return Result.Failure<CreatedResponse>(deliveryResult.Error);

        var delivery = deliveryResult.Value;
        await _deliveryRepository.AddAsync(delivery);
        await _unitOfWork.SaveChangesAsync();

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
            (DeliveryChannel)request.ChannelId,
            request.CarrierId
        );

        if (deliveryResult.IsFailure)
            return Result.Failure<UpdatedResponse>(deliveryResult.Error);

        await _deliveryRepository.UpdateAsync(delivery);
        await _unitOfWork.SaveChangesAsync();

        return delivery.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _deliveryRepository.HasActiveRelations(id))
            return Result.Failure(DeliveryErrors.HasActiveRelations);

        var delivery = await _deliveryRepository.GetByIdAsync(id);
        if (delivery is null)
            return Result.Failure(DeliveryErrors.NotFound);

        delivery.Deactivate();

        await _deliveryRepository.UpdateAsync(delivery);
        await _unitOfWork.SaveChangesAsync();

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
        var spec = new DeliverySelectListItemSpec();
        return await _deliveryRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<PagedList<DeliveryListResponse>>> GetDeliveriesListAsync(DeliveryListQuery queryParams, CancellationToken ct = default)
    {
        return await _deliveryRepository.GetDeliveriesListAsync(queryParams, ct);
    }

    public Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels()
        => Result.Success(EnumToSelectListMapper.EnumToSelectLists<DeliveryChannel>());

    public Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes()
        => Result.Success(EnumToSelectListMapper.EnumToSelectLists<ParcelSize>());

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync(CancellationToken ct)
    {
        var spec = new DeliveryOptionResponseSpec();
        var deliveries = await _deliveryRepository.GetListBySpecAsync(spec, ct);

        var response = new DeliveryOptionsResponse
        {
            ParcelLockerDeliveries = DeliveryMappings.MapDeliveries(deliveries, DeliveryChannel.ParcelLocker),
            CourierDeliveries = DeliveryMappings.MapDeliveries(deliveries, DeliveryChannel.Courier),
            PickupPointDeliveries = DeliveryMappings.MapDeliveries(deliveries, DeliveryChannel.PickupPoint)
        };

        return response;
    }

    //Helpers
    private static ParcelSize? GetEnumOrNull(int? id)
    {
        return id.HasValue
            ? (ParcelSize)id.Value
            : null;
    }

    public async Task<Result<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListPerOffer(Guid offerId, CancellationToken ct = default)
    {
        var spec = new DeliveryListResponseSpec(offerId);
        return await _deliveryRepository.GetListBySpecAsync(spec, ct);
    }
}
