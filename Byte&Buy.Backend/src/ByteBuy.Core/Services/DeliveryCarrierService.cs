using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class DeliveryCarrierService : IDeliveryCarrierService
{
    private readonly IDeliveryCarrierRepository _deliveryCarrierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeliveryCarrierService(IDeliveryCarrierRepository deliveryCarrierRepository,
        IUnitOfWork unitOfWork)
    {
        _deliveryCarrierRepository = deliveryCarrierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreatedResponse>> AddAsync(DeliveryCarrierAddRequest request)
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
        await _unitOfWork.SaveChangesAsync();

        return carrier.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, DeliveryCarrierUpdateRequest request)
    {
        var exists = await _deliveryCarrierRepository
           .ExistWithNameOrCodeAsync(request.Name, request.Code, id);

        if (exists)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.AlreadyExists);

        var carrier = await _deliveryCarrierRepository.GetByIdAsync(id);
        if (carrier is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        carrier.Update(
            request.Name,
            request.Code
        );

        await _deliveryCarrierRepository.UpdateAsync(carrier);
        await _unitOfWork.SaveChangesAsync();

        return carrier.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _deliveryCarrierRepository.HasActiveRelationsAsync(id))
            return Result.Failure(DeliveryCarrierErrors.HasActiveDeliveries);

        var carrier = await _deliveryCarrierRepository.GetByIdAsync(id);
        if (carrier is null)
            return Result.Failure(DeliveryCarrierErrors.NotFound);

        carrier.Deactivate();

        await _deliveryCarrierRepository.UpdateAsync(carrier);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var carrier = await _deliveryCarrierRepository.GetByIdAsync(id, ct);

        return carrier is null
            ? Result.Failure<DeliveryCarrierResponse>(Error.NotFound)
            : carrier.ToDeliveryCarrierResponse();
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    {
        var carriers = await _deliveryCarrierRepository.GetAllAsync(ct);

        return carriers
            .Select(c => c.ToSelectListItemResponse())
            .ToList();
    }

    public async Task<Result<IReadOnlyCollection<DeliveryCarrierResponse>>> GetDeliveryCarriersList(
        CancellationToken ct = default)
    {
        var carriers = await _deliveryCarrierRepository.GetAllAsync(ct);

        return carriers
            .Select(c => c.ToDeliveryCarrierResponse())
            .ToList();
    }
}
