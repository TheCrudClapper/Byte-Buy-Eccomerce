using ByteBuy.Services.DTO.Image;
using ByteBuy.Services.DTO.Item;
using ByteBuy.UI.ViewModels;
using System;
using System.Linq;
using System.Net.Http;

namespace ByteBuy.UI.Mappings;

public static class ItemMappings
{
    public static ItemAddRequest MapToRequest(this ItemPageViewModel vm)
    {
        var images = vm.Images.Select(i => new ImageAddRequest(i.AltText, i.FileName, i.FileStream)).ToList();
        return new ItemAddRequest(vm.SelectedCategory?.Id ?? Guid.Empty,
            vm.SelectedCondition?.Id ?? Guid.Empty,
            vm.Name,
            vm.Description,
            vm.StockQuantity,
            images);
    }
}
