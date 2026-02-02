using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.ModelsUI.RentOffer;
using ByteBuy.UI.ModelsUI.SaleOffer;
using ByteBuy.UI.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteBuy.UI.Mappings;

public static class OfferMappings
{
    public static SaleOfferAddRequest MapToSaleAddRequest(this OfferDialogViewModel vm)
    {
        return new SaleOfferAddRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.Quantity,
            vm.PricePerItem,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    public static SaleOfferUpdateRequest MapToSaleUpdateRequest(this OfferDialogViewModel vm)
    {
        return new SaleOfferUpdateRequest(
            vm.Quantity,
            vm.PricePerItem,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    public static RentOfferAddRequest MapToRentAddRequest(this OfferDialogViewModel vm)
    {
        return new RentOfferAddRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.Quantity,
            vm.PricePerDay,
            vm.RentalDays,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    public static RentOfferUpdateRequest MapToRentUpdateRequest(this OfferDialogViewModel vm)
    {
        return new RentOfferUpdateRequest(
            vm.Quantity,
            vm.PricePerDay,
            vm.RentalDays,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    private static List<Guid> MapParcelLockers(IEnumerable<ParcelLockerCarrierGroup> list)
        => list
            .SelectMany(p => p.Options)
            .Where(o => o.IsSelected)
            .Select(p => p.Id)
            .ToList();

    private static List<Guid> MapOtherDeliveries(OfferDialogViewModel vm)
        => vm.CourierDeliveries
            .Concat(vm.PickupPointDeliveries)
            .Where(d => d.IsSelected)
            .Select(d => d.Id)
            .ToList();

    public static RentOfferListItem ToListItem(this RentOfferListResponse response, int index)
    {
        return new RentOfferListItem
        {
            Id = response.Id,
            ItemName = response.ItemName,
            MaxRentalDays = response.MaxRentalDays,
            QuantityAvailable = response.QuantityAvailable,
            RowNumber = index + 1,
            PriceAndCurrencyPerDay = $"{response.Amount} {response.Currency}",
            CreatorEmail = response.CreatorEmail,
        };
    }

    public static SaleOfferListItem ToListItem(this SaleOfferListResponse response, int index)
    {
        return new SaleOfferListItem
        {
            Id = response.Id,
            ItemName = response.ItemName,
            QuantityAvailable = response.QuantityAvailable,
            RowNumber = index + 1,
            PriceAndCurrencyPerItem = $"{response.PricePerItem} {response.Currency}",
            CreatorEmail = response.CreatorEmail,
        };
    }

    /// <summary>
    /// Takes server response and maps given other and parcel locker ids to 
    /// selected state in view.
    /// </summary>
    /// <param name="vm"></param>
    /// <param name="otherDeliveriesIds"></param>
    /// <param name="parcelLockerIds"></param>
    private static void MapCommonDeliveries(this OfferDialogViewModel vm,
    IReadOnlyCollection<Guid> otherDeliveriesIds,
    IReadOnlyCollection<Guid>? parcelLockerIds)
    {
        var selectedIds = otherDeliveriesIds.ToHashSet();
        var parcelLockers = parcelLockerIds?.ToHashSet() ?? [];

        if (parcelLockers.Count > 0)
        {
            foreach (var parcelLockerList in vm.ParcelLockerGroups)
            {
                foreach (var deliveryOption in parcelLockerList.Options)
                {
                    if (parcelLockers.Contains(deliveryOption.Id))
                        deliveryOption.IsSelected = true;
                }
            }
        }

        foreach (var d in vm.CourierDeliveries)
            d.IsSelected = selectedIds.Contains(d.Id);

        foreach (var d in vm.PickupPointDeliveries)
            d.IsSelected = selectedIds.Contains(d.Id);
    }

    public static void MapFromRentResponse(this OfferDialogViewModel vm, RentOfferResponse response)
    {
        MapCommonDeliveries(vm, response.OtherDeliveriesIds, response.ParcelLockerDeliveries);
        vm.MaxRentalDays = response.MaxRentalDays;
        vm.CurrentAvaliableQuantity = response.QuantityAvailable;
        vm.PricePerDay = response.PricePerDay;
    }

    public static void MapFromSaleResponse(this OfferDialogViewModel vm, SaleOfferResponse response)
    {
        MapCommonDeliveries(vm, response.OtherDeliveriesIds, response.ParcelLockerDeliveries);
        vm.PricePerItem = response.PricePerItem;
        vm.CurrentAvaliableQuantity = response.QuantityAvailable;
    }
}
