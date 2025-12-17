using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.DeliveryCarrier;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class DeliveryCarrierService : IDeliveryCarrierService
{
    private readonly IDeliveryCarrierRepository _deliveryCarrierRepository;

    public DeliveryCarrierService(IDeliveryCarrierRepository deliveryCarrierRepository)
    {
        _deliveryCarrierRepository = deliveryCarrierRepository;
    }

    public async Task<Result<CreatedResponse>> AddDeliveryCarrier(DeliveryCarrierAddRequest request)
    {
        var exists = await _deliveryCarrierRepository
            .ExistWithNameOrCodeAsync(request.Name, request.Code);

        if (exists)
            return Result.Failure<CreatedResponse>(DeliveryCarrierErrors.AlreadyExists);

        var carrierResult = DeliveryCarrier.Create(
            request.Name,
            request.Code
        );

        if (carrierResult.IsFailure)
            return Result.Failure<CreatedResponse>(carrierResult.Error);

        var carrier = carrierResult.Value;

        await _deliveryCarrierRepository.AddAsync(carrier);
        await _deliveryCarrierRepository.CommitAsync();

        return carrier.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateDeliveryCarrier(
        Guid carrierId,
        DeliveryCarrierUpdateRequest request)
    {
        var exists = await _deliveryCarrierRepository
            .ExistWithNameOrCodeAsync(request.Name, request.Code, carrierId);

        if (exists)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.AlreadyExists);

        var carrier = await _deliveryCarrierRepository.GetByIdAsync(carrierId);
        if (carrier is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        carrier.Update(
            request.Name,
            request.Code
        );

        await _deliveryCarrierRepository.UpdateAsync(carrier);
        await _deliveryCarrierRepository.CommitAsync();

        return carrier.ToUpdatedResponse();
    }

    public async Task<Result> DeleteDeliveryCarrier(Guid carrierId)
    {
        if (await _deliveryCarrierRepository.HasActiveRelationsAsync(carrierId))
            return Result.Failure(DeliveryCarrierErrors.InUse);

        var carrier = await _deliveryCarrierRepository.GetByIdAsync(carrierId);
        if (carrier is null)
            return Result.Failure(DeliveryCarrierErrors.NotFound);

        carrier.Deactivate();

        await _deliveryCarrierRepository.UpdateAsync(carrier);
        await _deliveryCarrierRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<DeliveryCarrierResponse>> GetDeliveryCarrier(
        Guid carrierId,
        CancellationToken ct = default)
    {
        var carrier = await _deliveryCarrierRepository.GetByIdAsync(carrierId, ct);

        return carrier is null
            ? Result.Failure<DeliveryCarrierResponse>(Error.NotFound)
            : carrier.ToDeliveryCarrierResponse();
    }

    public async Task<Result<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersList(
        CancellationToken ct = default)
    {
        var carriers = await _deliveryCarrierRepository.GetAllAsync(ct);

        return carriers
            .Select(c => c.ToDeliveryCarrierResponse())
            .ToList();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(
        CancellationToken ct = default)
    {
        var carriers = await _deliveryCarrierRepository.GetAllAsync(ct);

        return carriers
            .Select(c => c.ToSelectListItemResponse())
            .ToList();
    }
}
