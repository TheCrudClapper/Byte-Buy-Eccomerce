using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ByteBuy.UI.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace ByteBuy.UI.Views;

public partial class ItemPageView : UserControl
{
    public ItemPageView()
    {
        InitializeComponent();

    }
    public ItemPageView(ItemPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private async void AddImages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var top = TopLevel.GetTopLevel(this);
        if (top is null) return;

        var files = await top.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Select images",
                AllowMultiple = true,
                FileTypeFilter = new[]
                {
                    FilePickerFileTypes.ImageJpg,
                    FilePickerFileTypes.ImagePng,
                }
            });

        var vm = (ItemPageViewModel)DataContext!;

        foreach (var file in files)
        {
            await using var stream = await file.OpenReadAsync();
            var mem = new MemoryStream();
            await stream.CopyToAsync(mem);
            mem.Position = 0;

            var bmp = new Bitmap(mem);
            mem.Position = 0;

            vm.Images.Add(new ViewModels.Shared.ItemImageViewModel(bmp, file.Name, mem));
        }
    }
}