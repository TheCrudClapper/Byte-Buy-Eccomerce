using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class ItemImageViewModel(Bitmap bitmap, string fileName, Stream fileStream) : ObservableObject
{
    public string FileName { get; } = fileName;
    public Bitmap? Preview { get; } = bitmap;

    [ObservableProperty]
    private string _altText = string.Empty;

    public Stream FileStream { get; } = fileStream;
}