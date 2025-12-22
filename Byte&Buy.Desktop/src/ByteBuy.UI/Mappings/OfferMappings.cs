using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.UI.ViewModels.Dialogs;
using System;
using System.Linq;

namespace ByteBuy.UI.Mappings;

public static class OfferMappings
{
    public static SaleOfferAddRequest MapToSaleAddRequest(this OfferDialogViewModel vm)
    {
        var parcelLockerDeliveries = vm.SelectedParcelLockerDeliveries
            .Where(p => p.SelectedOption is not null)
            .Select(p => p.SelectedOption!.Id)
            .ToList();

        var otherDeliveries = vm.SelectedOtherDeliveries
            .Select(d => d.Id)
            .ToList();

        return new SaleOfferAddRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerItem,
            parcelLockerDeliveries,
            otherDeliveries);
    }

    public static SaleOfferUpdateRequest MapToSaleUpdateRequest(this OfferDialogViewModel vm)
    {
        var parcelLockerDeliveries = vm.SelectedParcelLockerDeliveries
            .Where(p => p.SelectedOption is not null)
            .Select(p => p.SelectedOption!.Id)
            .ToList();

        var otherDeliveries = vm.SelectedOtherDeliveries
            .Select(d => d.Id)
            .ToList();

        return new SaleOfferUpdateRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerItem,
            parcelLockerDeliveries,
            otherDeliveries);
    }

    public static RentOfferAddRequest MapToRentAddRequest(this OfferDialogViewModel vm)
    {
        var parcelLockerDeliveries = vm.SelectedParcelLockerDeliveries
            .Where(p => p.SelectedOption is not null)
            .Select(p => p.SelectedOption!.Id)
            .ToList();

        var otherDeliveries = vm.SelectedOtherDeliveries
            .Select(d => d.Id)
            .ToList();

        return new RentOfferAddRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerDay,
            vm.MaxRentalDays,
            parcelLockerDeliveries,
            otherDeliveries);
    }

    public static RentOfferUpdateRequest MapToRentUpdateRequest(this OfferDialogViewModel vm)
    {
        var parcelLockerDeliveries = vm.SelectedParcelLockerDeliveries
            .Where(p => p.SelectedOption is not null)
            .Select(p => p.SelectedOption!.Id)
            .ToList();

        var otherDeliveries = vm.SelectedOtherDeliveries
            .Select(d => d.Id)
            .ToList();

        return new RentOfferUpdateRequest(
            vm.SelectedItem?.Id ?? Guid.Empty,
            vm.QuantityAvaliable,
            vm.PricePerDay,
            vm.MaxRentalDays,
            parcelLockerDeliveries,
            otherDeliveries);
    }

}
