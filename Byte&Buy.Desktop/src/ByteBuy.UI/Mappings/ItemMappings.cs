using ByteBuy.Core.DTO.Item;
using ByteBuy.Services.DTO.Image;
using ByteBuy.Services.DTO.Item;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.ViewModels;
using System;
using System.Linq;

namespace ByteBuy.UI.Mappings;

public static class ItemMappings
{
    public static ItemAddRequest MapAddToRequest(this ItemPageViewModel vm)
    {
        var images = vm.Images.Select(i => new ImageAddRequest(i.AltText, i.FileName, i.FileBytes)).ToList();
        return new ItemAddRequest(vm.SelectedCategory?.Id ?? Guid.Empty,
            vm.SelectedCondition?.Id ?? Guid.Empty,
            vm.Name,
            vm.Description,
            vm.StockQuantity,
            images);
    }

    public static ItemListItem ToListItem(this ItemListResponse response, int index)
        => new ItemListItem
        {
            CategoryName = response.CategoryName,
            ConditionName = response.ConditionName,
            Id = response.Id,
            ImagesCount = response.ImagesCount,
            Name = response.Name,
            RowNumber = index + 1,
            StockQuantity = response.StockQuantity
        };

    public static void MapFromResponse(this ItemPageViewModel vm, ItemResponse response)
    {
        vm.Description = response.Description;
        vm.CurrentStockQuantity = response.StockQuantity;
        vm.Name = response.Name;
        vm.SelectedCategory = vm.Categories
            .FirstOrDefault(c => c.Id == response.CategoryId);

        vm.SelectedCondition = vm.Conditions
            .FirstOrDefault(c => c.Id == response.ConditionId);
    }

    public static ItemUpdateRequest MapToUpdateRequest(this ItemPageViewModel vm)
    {
        var newImages = vm.Images
            .Where(i => i.IsNew && !i.IsDeleted)
            .Select(i => new ImageAddRequest(i.AltText, i.FileName, i.FileBytes))
            .ToList();

        var existingImages = vm.Images
            .Where(i => !i.IsNew)
            .Select(i => new ExistingImageUpdateRequest(i.Id ?? Guid.Empty, i.AltText, i.IsDeleted))
            .ToList();

        return new ItemUpdateRequest(
            vm.SelectedCategory?.Id ?? Guid.Empty,
            vm.SelectedCondition?.Id ?? Guid.Empty,
            vm.Name,
            vm.Description,
            vm.StockQuantity,
            newImages,
            existingImages);
    }
}
