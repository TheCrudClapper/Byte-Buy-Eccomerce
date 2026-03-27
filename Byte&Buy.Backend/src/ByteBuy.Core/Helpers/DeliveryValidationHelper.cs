using ByteBuy.Core.Domain.Deliveries;
using ByteBuy.Core.Domain.Deliveries.Enums;
using ByteBuy.Core.Domain.Offers.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Helpers;

public static class DeliveryValidationHelper
{
    /// <summary>
    /// Validates the specified parcel locker and other delivery identifiers, ensuring all referenced deliveries exist
    /// and meet required constraints.
    /// </summary>
    /// <remarks>This method ensures that all provided delivery identifiers correspond to existing deliveries.
    /// For parcel locker deliveries, it verifies that each delivery uses the parcel locker channel and that no carrier
    /// is associated with multiple parcel locker deliveries. If any validation fails, the result contains a relevant
    /// error.</remarks>
    /// <param name="parcelLockers">A collection of delivery identifiers representing parcel locker deliveries to validate. Can be null; if null, no
    /// parcel locker validation is performed.</param>
    /// <param name="otherDeliveries">A collection of delivery identifiers representing non-parcel locker deliveries to validate. Cannot be null.</param>
    /// <param name="deliveryRepository">The delivery repository used to retrieve and validate delivery entities by their identifiers.</param>
    /// <returns>A result containing a read-only collection of validated deliveries if all identifiers are valid and constraints
    /// are met; otherwise, a failure result with the appropriate error.</returns>
    public static async Task<Result<IReadOnlyCollection<Delivery>>> ValidateAllDeliveriesAsync(
        IEnumerable<Guid>? parcelLockers,
        IEnumerable<Guid> otherDeliveries,
        IDeliveryRepository deliveryRepository)
    {
        var parcelLockersIds = (parcelLockers ?? Enumerable.Empty<Guid>())
            .Distinct()
            .ToList();

        var otherDeliveriesIds = otherDeliveries
            .Distinct()
            .ToList();

        //Merging two lists in order to download just the deliveries we need
        var allIds = parcelLockersIds.Concat(otherDeliveriesIds).Distinct().ToList();
        if (allIds.Count == 0)
            return Result.Failure<IReadOnlyCollection<Delivery>>(OfferErrors.DeliveryRequired);

        var deliveries = await deliveryRepository.GetAllByIdsAsync(allIds);

        if (deliveries.Count != allIds.Count)
            return Result.Failure<IReadOnlyCollection<Delivery>>(Error.NotFound);

        if (parcelLockersIds.Count > 0)
        {
            var parcelDeliveries = deliveries
                .Where(d => parcelLockersIds.Contains(d.Id))
                .ToList();

            if (parcelDeliveries.Any(d => d.Channel != DeliveryChannel.ParcelLocker))
                return Result.Failure<IReadOnlyCollection<Delivery>>(OfferErrors.InvalidParcelLockerChannel);

            //only one parcel locker can be selected per carrier
            var multiplePerCarrier = parcelDeliveries
                .GroupBy(d => d.DeliveryCarrierId)
                .FirstOrDefault(g => g.Count() > 1);

            if (multiplePerCarrier is not null)
                return Result.Failure<IReadOnlyCollection<Delivery>>(OfferErrors.MultipleParcelLockersPerCarrier);
        }

        return Result.Success(deliveries);
    }
}
