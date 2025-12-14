using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using DialogHostAvalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public class DialogService(DialogFactory dialogFactory, Func<TopLevel?> topLevelAccessor) : IDialogService
{
    public async Task<object?> OpenDialogAsync(ApplicationDialogNames dialog, Func<DialogViewModel, Task>? init = null)
    {
        var viewModel = dialogFactory.GetDialogViewModel(dialog);
        var view = dialogFactory.GetView(dialog);

        if (init != null)
            await init.Invoke(viewModel);

        view.DataContext = viewModel;

        var result = await DialogHost.Show(view, "MainDialogHost");
        return result;
    }

    public async Task<List<ImageViewModel>> SelectImages(bool allowMultiple = false)
    {
        var tl = topLevelAccessor();
        if (tl is null)
            return [];

        var files = await tl.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = allowMultiple ? "Select files" : "Select file",
                AllowMultiple = allowMultiple,
                FileTypeFilter =
                [
                    FilePickerFileTypes.ImagePng,
                    FilePickerFileTypes.ImageJpg,
                ]
            });

        if (files.Count == 0)
            return [];

        var images = new List<ImageViewModel>();

        foreach (var file in files)
        {
            await using var stream = await file.OpenReadAsync();
            var mem = new MemoryStream();
            await stream.CopyToAsync(mem);
            mem.Position = 0;

            var bmp = new Bitmap(mem);
            mem.Position = 0;
            images.Add(new ImageViewModel(bmp, file.Name, mem));
        }

        return images;
    }
}
