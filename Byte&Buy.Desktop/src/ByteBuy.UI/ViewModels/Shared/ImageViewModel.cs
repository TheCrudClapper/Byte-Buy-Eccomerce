using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class ImageViewModel(Bitmap bitmap, string fileName, byte[] imageBytes) : ObservableValidator
{

    public string FileName { get; } = fileName;
    public Bitmap? Preview { get; } = bitmap;

    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _altText = string.Empty;

    //public Stream FileStream { get; } = fileStream;
    public byte[] FileBytes { get; } = imageBytes;

    public void Validate()
    {
        ValidateAllProperties();
    }

    partial void OnAltTextChanged(string? oldValue, string newValue)
    {
        ValidateProperty(newValue, nameof(AltText));
    }
}