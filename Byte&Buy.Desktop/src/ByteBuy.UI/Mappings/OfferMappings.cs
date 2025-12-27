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
            vm.QuantityAvaliable,
            vm.PricePerItem,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    public static SaleOfferUpdateRequest MapToSaleUpdateRequest(this OfferDialogViewModel vm)
    {
        return new SaleOfferUpdateRequest(
            vm.QuantityAvaliable,
            vm.PricePerItem,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    public static RentOfferAddRequest MapToRentAddRequest(this OfferDialogViewModel vm)
    {
        return new RentOfferAddRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerDay,
            vm.MaxRentalDays,
            MapParcelLockers(vm.ParcelLockerGroups),
            MapOtherDeliveries(vm));
    }

    public static RentOfferUpdateRequest MapToRentUpdateRequest(this OfferDialogViewModel vm)
    {
        return new RentOfferUpdateRequest(
            vm.QuantityAvaliable,
            vm.PricePerDay,
            vm.MaxRentalDays,
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

    public static void MapFromRentResponse(this OfferDialogViewModel vm, RentOfferResponse response)
    {
        var selectedIds = response.OtherDeliveriesIds.ToHashSet();

        if(response.ParcelLockerDeliveries is not null
            && response.ParcelLockerDeliveries.Count > 0){
            foreach (var d in vm.ParcelLockerGroups)
            {
                foreach (var d2 in d.Options)
                {
                    if (response.ParcelLockerDeliveries.Contains(d2.Id))
                        d2.IsSelected = true;
                }
            }
        }

        foreach (var d in vm.CourierDeliveries)
            d.IsSelected = selectedIds.Contains(d.Id);

        foreach (var d in vm.PickupPointDeliveries)
            d.IsSelected = selectedIds.Contains(d.Id);

        vm.MaxRentalDays = response.MaxRentalDays;
        vm.QuantityAvaliable = response.QuantityAvailable;
        vm.PricePerDay = response.PricePerDay;
    }

    public static void MapFromSaleResponse(this OfferDialogViewModel vm, SaleOfferResponse response)
    {
        var selectedIds = response.OtherDeliveriesIds.ToHashSet();

        foreach (var d in vm.CourierDeliveries)
            d.IsSelected = selectedIds.Contains(d.Id);

        foreach (var d in vm.PickupPointDeliveries)
            d.IsSelected = selectedIds.Contains(d.Id);

        vm.PricePerItem = response.PricePerItem;
        vm.QuantityAvaliable = response.QuantityAvailable;
    }
}
