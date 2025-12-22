using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.ViewModels.Dialogs;
using Microsoft.AspNetCore.Http;
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
}
