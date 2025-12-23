using AvaloniaEdit.Utils;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.ModelsUI.RentOffer;
using ByteBuy.UI.ModelsUI.SaleOffer;
using ByteBuy.UI.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            MapParcelLockers(vm.SelectedParcelLockerDeliveries),
            MapOtherDeliveries(vm.SelectedOtherDeliveries));
    }

    public static SaleOfferUpdateRequest MapToSaleUpdateRequest(this OfferDialogViewModel vm)
    {
        return new SaleOfferUpdateRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerItem,
            MapParcelLockers(vm.SelectedParcelLockerDeliveries),
            MapOtherDeliveries(vm.SelectedOtherDeliveries));
    }

    public static RentOfferAddRequest MapToRentAddRequest(this OfferDialogViewModel vm)
    {
        return new RentOfferAddRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerDay,
            vm.MaxRentalDays,
            MapParcelLockers(vm.SelectedParcelLockerDeliveries),
            MapOtherDeliveries(vm.SelectedOtherDeliveries));
    }

    public static RentOfferUpdateRequest MapToRentUpdateRequest(this OfferDialogViewModel vm)
    {
        return new RentOfferUpdateRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerDay,
            vm.MaxRentalDays,
            MapParcelLockers(vm.SelectedParcelLockerDeliveries),
            MapOtherDeliveries(vm.SelectedOtherDeliveries));

    }

    private static List<Guid> MapParcelLockers(IEnumerable<ParcelLockerCarrierGroup> list)
        => list.Where(p => p.SelectedOption is not null)
            .Select(p => p.SelectedOption!.Id)
            .ToList();

    private static List<Guid> MapOtherDeliveries(IEnumerable<DeliveryOptionResponse> list)
        => list.Select(d => d.Id).ToList();

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
        var allDeliveries = vm.PickupPointDeliveries
            .Concat(vm.CourierDeliveries)
            .ToList();

        var selected = allDeliveries
        .Where(d => response.OtherDeliveriesIds.Contains(d.Id))
        .ToList();

        vm.MaxRentalDays = response.MaxRentalDays;
        vm.QuantityAvaliable = response.QuantityAvailable;
        vm.PricePerDay = response.PricePerDay;

        vm.SelectedOtherDeliveries.Clear();

        foreach(var delivery in selected)
            vm.SelectedOtherDeliveries.Add(delivery);

        vm.ChangeOfferTypeCommand.Execute(vm);
    }

    public static void MapFromSaleResponse(this OfferDialogViewModel vm, SaleOfferResponse response)
    {
        var allDeliveries = vm.PickupPointDeliveries
           .Concat(vm.CourierDeliveries)
           .ToList();

        var selected = allDeliveries
        .Where(d => response.OtherDeliveriesIds.Contains(d.Id))
        .ToList();

        vm.PricePerItem = response.PricePerItem;
        vm.QuantityAvaliable = response.QuantityAvailable;

        vm.SelectedOtherDeliveries.Clear();
        foreach (var delivery in selected)
            vm.SelectedOtherDeliveries.Add(delivery);

    }
}
