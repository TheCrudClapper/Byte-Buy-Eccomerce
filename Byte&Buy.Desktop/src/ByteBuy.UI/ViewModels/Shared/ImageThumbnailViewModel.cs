using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class ImageThumbnailViewModel : ObservableObject
{
    public string ImagePath { get; set; } = null!;

    [ObservableProperty]
    public Bitmap? _preview; 

    public ImageThumbnailViewModel(string imagePath)
    {
        ImagePath = imagePath;
     
    }
}
